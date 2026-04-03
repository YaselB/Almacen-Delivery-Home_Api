using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlmacenApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateProductEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Unity = table.Column<string>(type: "text", nullable: false),
                    EndDate = table.Column<List<DateTime>>(type: "timestamp with time zone[]", nullable: false),
                    CreateByUser = table.Column<string>(type: "text", nullable: true),
                    CreateByAdmin = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.id);
                    table.ForeignKey(
                        name: "FK_Products_Admins_CreateByAdmin",
                        column: x => x.CreateByAdmin,
                        principalTable: "Admins",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Products_User_CreateByUser",
                        column: x => x.CreateByUser,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreateByAdmin",
                table: "Products",
                column: "CreateByAdmin");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreateByUser",
                table: "Products",
                column: "CreateByUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
