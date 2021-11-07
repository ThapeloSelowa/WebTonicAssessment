using Microsoft.EntityFrameworkCore.Migrations;

namespace WebTonicAssessmentAPI.Migrations
{
    public partial class tables_updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CapturedByUserId",
                table: "StudentRecords",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Client",
                table: "StudentRecords",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Client",
                table: "ErrorLogs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ErrorLogs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapturedByUserId",
                table: "StudentRecords");

            migrationBuilder.DropColumn(
                name: "Client",
                table: "StudentRecords");

            migrationBuilder.DropColumn(
                name: "Client",
                table: "ErrorLogs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ErrorLogs");
        }
    }
}
