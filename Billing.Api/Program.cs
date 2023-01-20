using Billing.Api.Infrastructure;
using Billing.Api.Models.Customer;
using Billing.Api.Settings;
using Billing.Business.Dto.Customer;
using Billing.Business.Engines;
using Billing.Business.Engines.Contracts;
using Billing.Business.Extensions;
using Billing.Data;
using Billing.Data.Contracts;
using Billing.Data.Entities;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


#region Configuration Swagger

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Billing API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

#endregion Configuration Swagger

builder.Services.AddDbContext<ApplicationContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Billing.Api"));
    });

#region Configuration DI Repository

builder.Services.AddScoped<IDataRepositoryFactory, DataRepositoryFactory>();

//New instance for injection
builder.Services.AddTransient(typeof(IDataRepository<>), typeof(Repository<>));

//Custom Repositories injection     
var repositoryTypes =
    typeof(Repository<>).Assembly
                        .ExportedTypes
                        .Where(x => typeof(IDataRepository).IsAssignableFrom(x) &&
                                    !x.IsInterface &&
                                    !x.IsAbstract).ToList();

repositoryTypes.ForEach(repositoryType =>
{
    var contract = repositoryType.GetInterface($"I{repositoryType.Name}");

    if (contract != null)
        builder.Services.AddScoped(contract, repositoryType);
});

#endregion Configuration DI Repository

#region Configuration DI BusinessEngine

builder.Services.AddScoped<IBusinessEngineFactory, BusinessEngineFactory>();
var engineTypes =
    typeof(BusinessEngineFactory).Assembly
                                 .ExportedTypes
                                 .Where(x => typeof(IBusinessEngine).IsAssignableFrom(x) &&
                                             !x.IsInterface &&
                                             !x.IsAbstract).ToList();

engineTypes.ForEach(engineType =>
{
    builder.Services.AddScoped(engineType.GetInterface($"I{engineType.Name}"), engineType);
});

#endregion Configuration DI BusinessEngine

#region Configuration Mapper

var config = TypeAdapterConfig.GlobalSettings;

TypeAdapterConfig<CustomerPayloadDto, Customer>.NewConfig()
    .Map(source => source.Password, dest => dest.Password.Base64Encode());

TypeAdapterConfig<CustomerLoginViewModel, CustomerLoginPayloadDto>.NewConfig()
    .Map(source => source.Password, dest => dest.Password.Base64Encode());

builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

#endregion Configuration Mapper

#region Jwt

var jwtSettings = new JwtSettings();

builder.Configuration.GetSection("Jwt").Bind(jwtSettings);

if (!builder.Services.Any(x => x.ServiceType == typeof(JwtSettings)))
    builder.Services.AddSingleton(jwtSettings);

//Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
    };
});

builder.Services.AddAuthorization();

#endregion Jwt


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
