namespace ParsMedeq.Infrastructure.Persistance.DbContexts.Extensions;
public static class EFExtensions
{
    public static PropertyBuilder<string> IsSlugColumn(this PropertyBuilder<string> src) =>
        src.IsUnicode(true).HasMaxLength(255);

    public static PropertyBuilder<string> IsTitleColumn(this PropertyBuilder<string> src) =>
        src.IsUnicode(true).HasMaxLength(255);
}

