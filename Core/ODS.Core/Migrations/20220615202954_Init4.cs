using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ODS.Core.Migrations
{
    public partial class Init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                schema: "Domain",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "Domain",
                table: "Donors");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "Domain",
                table: "Orphanages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                schema: "Domain",
                table: "Orphanages");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "Domain",
                table: "Donors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "Domain",
                table: "Donors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
