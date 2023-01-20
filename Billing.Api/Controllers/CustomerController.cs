using Billing.Api.Exceptions;
using Billing.Api.Models;
using Billing.Api.Models.Customer;
using Billing.Api.Settings;
using Billing.Business.Dto.Customer;
using Billing.Business.Engines;
using Billing.Business.Engines.Contracts;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Billing.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ApiControllerBase
    {
        private readonly IBusinessEngineFactory _businessEngineFactory;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerController> _logger;
        private readonly JwtSettings _jwtSettings;

        public CustomerController(ILogger<CustomerController> logger, IBusinessEngineFactory businessEngineFactory, IMapper mapper, JwtSettings jwtSettings)
        {
            _logger = logger;
            _businessEngineFactory = businessEngineFactory;
            _mapper = mapper;
            _jwtSettings = jwtSettings;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult> LoginAsync([FromBody] CustomerLoginViewModel customerLogin)
        {
            if (!ModelState.IsValid)
                throw new ArgumentException("Error de login");

            var customer = await Authenticate(customerLogin);

            if (customer != null)
            {
                var token = GenerateToken(customer);

                return Ok(new TokenResponseModel
                {
                    CustomerId = customer.Id,
                    AccessToken = token,
                    ExpiresIn = _jwtSettings.ExpiresOn
                });
            }

            throw new ArgumentException("Error de login");
        }

        /// <summary>
        /// To authenticate the customer
        /// </summary>
        private async Task<CustomerResultDto> Authenticate(CustomerLoginViewModel customerLogin)
        {
            var payload = _mapper.Map<CustomerLoginPayloadDto>(customerLogin);
            var service = _businessEngineFactory.Get<ICustomerEngine>();

            var result = await service.AuthenticateAsync(payload);

            return result;
        }

        /// <summary>
        /// To generate token
        /// </summary>
        private string GenerateToken(CustomerResultDto customer)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("name", customer.Name ?? string.Empty),
                new Claim("email", customer.Email ?? string.Empty),
                new Claim(ClaimTypes.Role, "Customer")
            };
            var token = new JwtSecurityToken(_jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(_jwtSettings.ExpiresOn),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<CustomerResultDto> GetAsync(Guid id)
        {
            var engine = _businessEngineFactory.Get<ICustomerEngine>();

            var customer = await engine.GetAsync(id);

            return customer;
        }

        [HttpGet]
        [Route("list")]
        [AllowAnonymous]
        public async Task<IEnumerable<CustomerResultDto>> GetAllAsync()
        {
            var engine = _businessEngineFactory.Get<ICustomerEngine>();

            var customers = await engine.GetAllAsync();

            return customers;
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<CustomerResultDto> CreateCustomerAsync([FromBody] CustomerPayloadViewModel model)
        {
            if (!ModelState.IsValid)
                throw new ModelValidationException("Error modelo CreateCustomerAsync", GetModelErrors(ModelState));

            var payload = _mapper.Map<CustomerPayloadDto>(model);
            var service = _businessEngineFactory.Get<ICustomerEngine>();

            var result = await service.CreateCustomerAsync(payload);

            return result;
        }

        [HttpPut]
        [Route("")]
        [AllowAnonymous]
        public async Task<CustomerResultDto> UpdateCustomerAsync([FromBody] CustomerPayloadViewModel model)
        {
            if (!ModelState.IsValid)
                throw new ModelValidationException("Error modelo UpdateCustomerAsync", GetModelErrors(ModelState));

            var payload = _mapper.Map<CustomerPayloadDto>(model);
            var service = _businessEngineFactory.Get<ICustomerEngine>();

            var result = await service.UpdateCustomerAsync(payload);

            return result;
        }

        [HttpDelete]
        [Route("")]
        [AllowAnonymous]
        public async Task<CustomerResultDto> DeleteCustomerAsync(Guid id)
        {
            if (!ModelState.IsValid)
                throw new ModelValidationException("Error modelo DeleteCustomerAsync", GetModelErrors(ModelState));

            var service = _businessEngineFactory.Get<ICustomerEngine>();

            var result = await service.DeleteCustomerAsync(id);

            return result;
        }
    }
}
