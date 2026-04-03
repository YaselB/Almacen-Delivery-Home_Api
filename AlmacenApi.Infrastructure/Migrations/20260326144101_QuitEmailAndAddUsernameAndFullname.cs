using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlmacenApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class QuitEmailAndAddUsernameAndFullname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "User",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Admins",
                newName: "Username");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Admins",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "User",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Admins",
                newName: "email");
        }
    }
}
