using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pronia_BackEnd_Project.Migrations
{
    public partial class NewColumnAppUserRemmebe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RememberMe",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RememberMe",
                table: "AspNetUsers");
        }
    }
}
