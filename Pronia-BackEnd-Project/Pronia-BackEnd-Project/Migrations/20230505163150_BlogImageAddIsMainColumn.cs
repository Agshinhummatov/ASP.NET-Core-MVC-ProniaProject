using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pronia_BackEnd_Project.Migrations
{
    public partial class BlogImageAddIsMainColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "BlogImages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "BlogImages");
        }
    }
}
