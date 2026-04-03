using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlmacenApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductOutEntityToAddCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Customer",
                table: "ProductOut",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Customer",
                table: "ProductOut");
        }
    }
}
