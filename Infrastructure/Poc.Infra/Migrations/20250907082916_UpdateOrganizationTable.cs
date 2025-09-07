using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poc.Infra.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrganizationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AwsOrganization",
                type: "bit",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagementAccount",
                table: "AwsOrganization",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagementAccount",
                table: "AwsOrganization");

            migrationBuilder.AlterColumn<string>(
                name: "IsDeleted",
                table: "AwsOrganization",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
