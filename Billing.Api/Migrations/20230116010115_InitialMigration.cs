using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Billing.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Domicilio = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Habilitado = table.Column<bool>(type: "bit", nullable: false),
                    IdCiudad = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_City_IdCiudad",
                        column: x => x.IdCiudad,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Detalle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Importe = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    IdCliente = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_Customer_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_IdCiudad",
                table: "Customer",
                column: "IdCiudad");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_IdCliente",
                table: "Invoice",
                column: "IdCliente");

            #region CustomInsert

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "Nombre" },
                values: new object[] { "e023b286-7e9e-48cf-8ad6-ea2ccc293b5d", "Brooklyn" });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "Nombre" },
                values: new object[] { "b6a011d5-9096-4e3b-94fe-a82467b16119", "California" });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "Nombre" },
                values: new object[] { "6ada6815-5af6-4d96-a845-7ce721a6edaa", "Cameron" });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "Nombre" },
                values: new object[] { "4f949796-22ad-432f-b656-180dd0ca5db1", "Dallas" });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "Nombre" },
                values: new object[] { "59f3926a-7f51-455e-afe1-2d862726921f", "Dexter" });

            #endregion CustomInsert
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "City");
        }
    }
}
