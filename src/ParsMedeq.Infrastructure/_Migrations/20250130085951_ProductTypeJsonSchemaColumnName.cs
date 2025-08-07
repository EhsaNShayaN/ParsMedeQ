using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.Infrastructure._Migrations
{
    /// <inheritdoc />
    public partial class ProductTypeJsonSchemaColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Schema",
                table: "ProductType",
                newName: "JsonSchema");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JsonSchema",
                table: "ProductType",
                newName: "Schema");
        }
    }
}
