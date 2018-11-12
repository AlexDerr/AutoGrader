using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoGrader.Migrations
{
    public partial class NoPass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Instructors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Instructors",
                nullable: true);
        }
    }
}
