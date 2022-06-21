using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ODS.Core.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FileUploads",
                schema: "Domain",
                table: "FileUploads");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "Domain",
                table: "Donors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileUploads",
                schema: "Domain",
                table: "FileUploads",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FileUploads_OrphanageId",
                schema: "Domain",
                table: "FileUploads",
                column: "OrphanageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FileUploads",
                schema: "Domain",
                table: "FileUploads");

            migrationBuilder.DropIndex(
                name: "IX_FileUploads_OrphanageId",
                schema: "Domain",
                table: "FileUploads");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Domain",
                table: "Donors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileUploads",
                schema: "Domain",
                table: "FileUploads",
                columns: new[] { "OrphanageId", "Id" });
        }
    }
}
