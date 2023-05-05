using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pronia_BackEnd_Project.Migrations
{
    public partial class NewColumnBrandRowImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Brands",
                newName: "Image");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Brands",
                newName: "Name");
        }
    }
}
