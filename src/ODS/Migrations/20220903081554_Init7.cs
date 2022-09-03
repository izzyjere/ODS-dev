using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ODS.Core.Migrations
{
    public partial class Init7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "Domain",
                table: "Donors",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "Domain",
                table: "Donors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "Domain",
                table: "Donors");

            migrationBuilder.RenameColumn(
                name: "LastName",
                schema: "Domain",
                table: "Donors",
                newName: "Name");
        }
    }
}
