namespace EShop.Domain.Helpers;
public static class UlidHelper
{
    public static string CreateNewId() => Ulid.NewUlid().ToString();
}
