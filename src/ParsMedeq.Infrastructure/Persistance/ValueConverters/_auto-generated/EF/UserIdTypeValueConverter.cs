namespace ParsMedeQ.Infrastructure.Persistance.ValueConverters;
sealed class intValueConverter : ValueConverter<int, int>
{
    public intValueConverter() : base(
        src => src,
        value => value
    )
    { }
}


sealed class intValueComparer : ValueComparer<int>
{
    public intValueComparer() : base(
        (a, b) => a.Equals(b),
        a => a.GetHashCode())
    { }
}

