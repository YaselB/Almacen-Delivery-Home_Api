using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlmacenApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateComboOutEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComboOut",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    ComboName = table.Column<string>(type: "text", nullable: false),
                    OutMotive = table.Column<string>(type: "text", nullable: false),
                    ComboId = table.Column<string>(type: "text", nullable: false),
                    AdminId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    UpdateComboid = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboOut", x => x.id);
                    table.ForeignKey(
                        name: "FK_ComboOut_Admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admins",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboOut_Combo_ComboId",
                        column: x => x.ComboId,
                        principalTable: "Combo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboOut_Combo_UpdateComboid",
                        column: x => x.UpdateComboid,
                        principalTable: "Combo",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ComboOut_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOut",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    OutMotive = table.Column<string>(type: "text", nullable: false),
                    AdminId = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    ProductId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOut", x => x.id);
                    table.ForeignKey(
                        name: "FK_ProductOut_Admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admins",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ProductOut_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOut_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComboOut_AdminId",
                table: "ComboOut",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_ComboOut_ComboId",
                table: "ComboOut",
                column: "ComboId");

            migrationBuilder.CreateIndex(
                name: "IX_ComboOut_UpdateComboid",
                table: "ComboOut",
                column: "UpdateComboid");

            migrationBuilder.CreateIndex(
                name: "IX_ComboOut_UserId",
                table: "ComboOut",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOut_AdminId",
                table: "ProductOut",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOut_ProductId",
                table: "ProductOut",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOut_UserId",
                table: "ProductOut",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComboOut");

            migrationBuilder.DropTable(
                name: "ProductOut");
        }
    }
}
