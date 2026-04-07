using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlmacenApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProviderToProductEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Provider",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Provider",
                table: "Products");
        }
    }
}
