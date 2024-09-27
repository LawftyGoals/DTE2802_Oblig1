using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObligEnBlog.Migrations {
    public partial class userUpdate : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Comment",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "BlogPost",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Blog",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_OwnerId",
                table: "Comment",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPost_OwnerId",
                table: "BlogPost",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_OwnerId",
                table: "Blog",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_AspNetUsers_OwnerId",
                table: "Blog",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPost_AspNetUsers_OwnerId",
                table: "BlogPost",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_OwnerId",
                table: "Comment",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_AspNetUsers_OwnerId",
                table: "Blog");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPost_AspNetUsers_OwnerId",
                table: "BlogPost");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_OwnerId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_OwnerId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_BlogPost_OwnerId",
                table: "BlogPost");

            migrationBuilder.DropIndex(
                name: "IX_Blog_OwnerId",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "BlogPost");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Blog");
        }
    }
}
