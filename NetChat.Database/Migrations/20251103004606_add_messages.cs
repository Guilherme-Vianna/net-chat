using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetChat.Database.Migrations
{
    /// <inheritdoc />
    public partial class add_messages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "messages");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "messages");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "messages",
                newName: "Data");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Data",
                table: "messages",
                newName: "PasswordHash");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "messages",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "messages",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
