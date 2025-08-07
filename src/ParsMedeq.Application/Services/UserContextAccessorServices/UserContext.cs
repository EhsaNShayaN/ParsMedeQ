using EShop.Domain.Types.UserId;

namespace EShop.Application.Services.UserContextAccessorServices;

public sealed record UserContext(UserIdType Id, UserInfo UserInfo)
{
    UserProfileItem? _profile = null;

    public readonly static UserContext Guest = new(UserIdType.FromDb(0), UserInfo.Empty);

    public UserProfileItem Profile
    {
        get
        {
            if (this.Id.Value.Equals(0)) new UserProfileItem(UserContextProfileId.Serialize(new UserContextProfileId(UserIdType.FromDb(0))));
            this._profile ??= new UserProfileItem(UserContextProfileId.Serialize(new UserContextProfileId(this.Id)));
            return this._profile;
        }
    }

    public static UserContext Create(UserIdType Id, UserInfo value) => new(Id, value);
}
