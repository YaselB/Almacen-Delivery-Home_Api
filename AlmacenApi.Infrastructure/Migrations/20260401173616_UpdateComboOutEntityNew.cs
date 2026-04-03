using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlmacenApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateComboOutEntityNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComboOut_Combo_UpdateComboid",
                table: "ComboOut");

            migrationBuilder.DropIndex(
                name: "IX_ComboOut_UpdateComboid",
                table: "ComboOut");

            migrationBuilder.DropColumn(
                name: "UpdateComboid",
                table: "ComboOut");

            migrationBuilder.AddColumn<List<string>>(
                name: "ProductsId",
                table: "ComboOut",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<List<int>>(
                name: "Quantity",
                table: "ComboOut",
                type: "integer[]",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductsId",
                table: "ComboOut");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ComboOut");

            migrationBuilder.AddColumn<string>(
                name: "UpdateComboid",
                table: "ComboOut",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComboOut_UpdateComboid",
                table: "ComboOut",
                column: "UpdateComboid");

            migrationBuilder.AddForeignKey(
                name: "FK_ComboOut_Combo_UpdateComboid",
                table: "ComboOut",
                column: "UpdateComboid",
                principalTable: "Combo",
                principalColumn: "id");
        }
    }
}
