namespace ParsMedeQ.Domain.Helpers;

public static class HashIdsHelper
{
    public readonly static Hashids Instance = new("hPIdRyf0apajXotUycvGH4AyZnce96M0", 6, seps: "SRHparsSRHmedeq");
    public readonly static HashIdsHexSerializer HexSerializer = new(Instance);
}

