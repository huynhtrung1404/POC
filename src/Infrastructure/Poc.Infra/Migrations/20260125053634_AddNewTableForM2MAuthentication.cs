using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poc.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTableForM2MAuthentication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "M2MAuthentication",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ClientSecret = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Scope = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrantType = table.Column<int>(type: "int", nullable: false),
                    LoginType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "varchar(50)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M2MAuthentication", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_M2MAuthentication_ClientId_LoginType",
                table: "M2MAuthentication",
                columns: new[] { "ClientId", "LoginType" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "M2MAuthentication");
        }
    }
}
