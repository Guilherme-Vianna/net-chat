using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetChat.Database.Migrations
{
    /// <inheritdoc />
    public partial class fix_cascade_delete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_tags_tags_TagId",
                table: "user_tags");

            migrationBuilder.AddForeignKey(
                name: "FK_user_tags_tags_TagId",
                table: "user_tags",
                column: "TagId",
                principalTable: "tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_tags_tags_TagId",
                table: "user_tags");

            migrationBuilder.AddForeignKey(
                name: "FK_user_tags_tags_TagId",
                table: "user_tags",
                column: "TagId",
                principalTable: "tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
