using ParsMedeQ.Domain.Helpers;

namespace ParsMedeQ.Application.Services.UserContextAccessorServices;

public readonly record struct UserContextProfileId(int UserId)
{
    public static string Serialize(UserContextProfileId item)
    {
        return HashIdsHelper.HexSerializer.Serialize($"{item.UserId}");
    }
    public string Serialize()
    {
        return Serialize(this);
    }
    public static UserContextProfileId Deserialize(string src)
    {
        return HashIdsHelper.HexSerializer.Deserialize(src, item =>
        {
            var parts = item.Split('|');
            return new UserContextProfileId(Convert.ToInt32(parts[0]));
        });
    }
}
