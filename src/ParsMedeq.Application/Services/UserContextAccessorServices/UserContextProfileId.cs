using ParsMedeQ.Domain.Helpers;
using ParsMedeQ.Domain.Types.UserId;

namespace ParsMedeQ.Application.Services.UserContextAccessorServices;

public readonly record struct UserContextProfileId(UserIdType UserId)
{
    public static string Serialize(UserContextProfileId item)
    {
        return HashIdsHelper.HexSerializer.Serialize($"{item.UserId.Value}");
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
            return new UserContextProfileId(UserIdType.FromDb(Convert.ToInt32(parts[0])));
        });
    }
}
