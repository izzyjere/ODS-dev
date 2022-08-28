using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ODS.Core.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "OrphanageNeeds",
                newName: "OrphanageNeeds",
                newSchema: "Domain");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "OrphanageNeeds",
                schema: "Domain",
                newName: "OrphanageNeeds");
        }
    }
}
