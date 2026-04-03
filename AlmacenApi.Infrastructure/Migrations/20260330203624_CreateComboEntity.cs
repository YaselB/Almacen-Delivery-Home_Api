using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlmacenApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateComboEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ComboId",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Combo",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AdminId = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Combo", x => x.id);
                    table.ForeignKey(
                        name: "FK_Combo_Admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admins",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Combo_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ComboId",
                table: "Products",
                column: "ComboId");

            migrationBuilder.CreateIndex(
                name: "IX_Combo_AdminId",
                table: "Combo",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Combo_UserId",
                table: "Combo",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Combo_ComboId",
                table: "Products",
                column: "ComboId",
                principalTable: "Combo",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Combo_ComboId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Combo");

            migrationBuilder.DropIndex(
                name: "IX_Products_ComboId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ComboId",
                table: "Products");
        }
    }
}
