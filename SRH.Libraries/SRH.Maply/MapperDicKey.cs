namespace SRH.Maply;

public readonly struct MapperDicKey : IEquatable<MapperDicKey>
{
    public Type Source { get; }

    public Type Destination { get; }

    public bool Equals(MapperDicKey other)
    {
        if (Source == other.Source)
        {
            return Destination == other.Destination;
        }

        return false;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;

        if (!(obj is MapperDicKey))
        {
            return false;
        }

        return Equals((MapperDicKey)obj);
    }

    public override int GetHashCode()
    {
        return Source.GetHashCode() << 16 ^ Destination.GetHashCode() & 0xFFFF;
    }

    public static bool operator ==(MapperDicKey left, MapperDicKey right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(MapperDicKey left, MapperDicKey right)
    {
        return !left.Equals(right);
    }

    public MapperDicKey(Type source, Type destination)
    {
        Source = source;
        Destination = destination;
    }
}
