namespace ParsMedeQ.Application.Services.UserContextAccessorServices;

public record UserProfileItem(string ProfileId)
{
    private UserContextProfileId? _id = null;
    public readonly static UserProfileItem Empty = new(string.Empty);

    public UserContextProfileId Id
    {
        get
        {
            if (this._id is null)
            {
                this._id = UserContextProfileId.Deserialize(this.ProfileId);
            }
            return this._id.Value;
        }

    }

    public bool IsEmpty() => this.Equals(Empty) || string.IsNullOrWhiteSpace(this.ProfileId);
}
