using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ODS.Core.Migrations
{
    public partial class Init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Domain",
                table: "OrphanageNeeds");

            migrationBuilder.AddColumn<DateTime>(
                name: "MonthStart",
                schema: "Domain",
                table: "OrphanageNeeds",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthStart",
                schema: "Domain",
                table: "OrphanageNeeds");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Domain",
                table: "OrphanageNeeds",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
