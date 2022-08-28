using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ODS.Core.Migrations
{
    public partial class Init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpharnageId",
                schema: "Domain",
                table: "OrphanageNeeds");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OpharnageId",
                schema: "Domain",
                table: "OrphanageNeeds",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
