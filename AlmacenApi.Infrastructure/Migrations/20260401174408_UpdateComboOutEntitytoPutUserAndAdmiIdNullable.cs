using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlmacenApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateComboOutEntitytoPutUserAndAdmiIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComboOut_Admins_AdminId",
                table: "ComboOut");

            migrationBuilder.DropForeignKey(
                name: "FK_ComboOut_User_UserId",
                table: "ComboOut");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ComboOut",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "AdminId",
                table: "ComboOut",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_ComboOut_Admins_AdminId",
                table: "ComboOut",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComboOut_User_UserId",
                table: "ComboOut",
                column: "UserId",
                principalTable: "User",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComboOut_Admins_AdminId",
                table: "ComboOut");

            migrationBuilder.DropForeignKey(
                name: "FK_ComboOut_User_UserId",
                table: "ComboOut");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ComboOut",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AdminId",
                table: "ComboOut",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ComboOut_Admins_AdminId",
                table: "ComboOut",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComboOut_User_UserId",
                table: "ComboOut",
                column: "UserId",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
