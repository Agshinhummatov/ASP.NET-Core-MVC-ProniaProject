using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pronia_BackEnd_Project.Migrations
{
    public partial class NewTableClientColumnDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Clients");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Clients");

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
