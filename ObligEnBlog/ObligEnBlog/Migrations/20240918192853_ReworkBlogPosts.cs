using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObligEnBlog.Migrations
{
    public partial class ReworkBlogPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPost_Blog_BlogParentBlogId",
                table: "BlogPost");

            migrationBuilder.DropIndex(
                name: "IX_BlogPost_BlogParentBlogId",
                table: "BlogPost");

            migrationBuilder.RenameColumn(
                name: "BlogParentBlogId",
                table: "BlogPost",
                newName: "BlogParentId");

            migrationBuilder.AddColumn<int>(
                name: "BlogId",
                table: "BlogPost",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogPost_BlogId",
                table: "BlogPost",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPost_Blog_BlogId",
                table: "BlogPost",
                column: "BlogId",
                principalTable: "Blog",
                principalColumn: "BlogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPost_Blog_BlogId",
                table: "BlogPost");

            migrationBuilder.DropIndex(
                name: "IX_BlogPost_BlogId",
                table: "BlogPost");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "BlogPost");

            migrationBuilder.RenameColumn(
                name: "BlogParentId",
                table: "BlogPost",
                newName: "BlogParentBlogId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPost_BlogParentBlogId",
                table: "BlogPost",
                column: "BlogParentBlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPost_Blog_BlogParentBlogId",
                table: "BlogPost",
                column: "BlogParentBlogId",
                principalTable: "Blog",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
