using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTonicAssessmentAPI.Migrations
{
    public partial class audittable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserEmail = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    Client = table.Column<string>(nullable: true),
                    Change = table.Column<string>(nullable: true),
                    Change_Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audits");
        }
    }
}
