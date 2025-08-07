using EShop.Infrastructure.Persistance.DbContexts.Configurations;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Infrastructure.Persistance.DbContexts.Seeds;
internal static class EshopSeedDataHelper
{
    internal static void Seed_ProductType(this MigrationBuilder builder)
    {
        builder.InsertData(
            table: TableNames.ProductType,
            columns: new[]
            {
                nameof(ProductType.Slug),
                nameof(ProductType.Title),
                nameof(ProductType.JsonSchema),
                nameof(ProductType.Variations)
            },
            values: new object[,] {
                { "mobile", "موبایل", "{}", ProductTypeVariations.Color.ToString() },
                { "console-game", "کنسول بازی", "{}", string.Empty }
            });
            
    }
}
