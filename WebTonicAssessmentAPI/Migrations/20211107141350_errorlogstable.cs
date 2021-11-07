using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTonicAssessmentAPI.Migrations
{
    public partial class errorlogstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ErrorLogs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Section = table.Column<string>(nullable: true),
                    Method = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Date_Stamp = table.Column<DateTime>(nullable: false),
                    Computer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLogs", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorLogs");
        }
    }
}
