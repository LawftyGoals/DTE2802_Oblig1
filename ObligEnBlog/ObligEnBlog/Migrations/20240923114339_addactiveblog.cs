using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObligEnBlog.Migrations
{
    public partial class addactiveblog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Blog",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Blog");
        }
    }
}
