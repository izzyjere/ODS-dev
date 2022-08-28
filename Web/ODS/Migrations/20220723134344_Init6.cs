using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ODS.Core.Migrations
{
    public partial class Init6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Founder",
                schema: "Domain",
                table: "Orphanages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                schema: "Domain",
                table: "Donations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Founder",
                schema: "Domain",
                table: "Orphanages");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                schema: "Domain",
                table: "Donations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
