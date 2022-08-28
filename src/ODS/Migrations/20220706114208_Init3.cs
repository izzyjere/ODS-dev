using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ODS.Core.Migrations
{
    public partial class Init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Domain",
                table: "OrphanageNeeds",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Raised",
                schema: "Domain",
                table: "OrphanageNeeds",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Domain",
                table: "OrphanageNeeds");

            migrationBuilder.DropColumn(
                name: "Raised",
                schema: "Domain",
                table: "OrphanageNeeds");
        }
    }
}
