using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetChat.Database.Migrations
{
    /// <inheritdoc />
    public partial class rename_table_user_friends : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFriends_users_FriendId",
                table: "UserFriends");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFriends_users_UserId",
                table: "UserFriends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFriends",
                table: "UserFriends");

            migrationBuilder.RenameTable(
                name: "UserFriends",
                newName: "user_friends");

            migrationBuilder.RenameIndex(
                name: "IX_UserFriends_UserId",
                table: "user_friends",
                newName: "IX_user_friends_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFriends_FriendId",
                table: "user_friends",
                newName: "IX_user_friends_FriendId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_friends",
                table: "user_friends",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_friends_users_FriendId",
                table: "user_friends",
                column: "FriendId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_user_friends_users_UserId",
                table: "user_friends",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_friends_users_FriendId",
                table: "user_friends");

            migrationBuilder.DropForeignKey(
                name: "FK_user_friends_users_UserId",
                table: "user_friends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_friends",
                table: "user_friends");

            migrationBuilder.RenameTable(
                name: "user_friends",
                newName: "UserFriends");

            migrationBuilder.RenameIndex(
                name: "IX_user_friends_UserId",
                table: "UserFriends",
                newName: "IX_UserFriends_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_user_friends_FriendId",
                table: "UserFriends",
                newName: "IX_UserFriends_FriendId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFriends",
                table: "UserFriends",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriends_users_FriendId",
                table: "UserFriends",
                column: "FriendId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriends_users_UserId",
                table: "UserFriends",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
