using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlmacenApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPermissionsJson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PermissionsJson",
                table: "Admins",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermissionsJson",
                table: "Admins");
        }
    }
}
