namespace EShop.Domain.Helpers;

public static class HashIdsHelper
{
    public readonly static Hashids Instance = new Hashids("hPIdRyf0apajXotUycvGH4AyZnce96M0", 6, seps: "SRHeshopSRHEShop");
    public readonly static HashIdsHexSerializer HexSerializer = new HashIdsHexSerializer(Instance);
}

