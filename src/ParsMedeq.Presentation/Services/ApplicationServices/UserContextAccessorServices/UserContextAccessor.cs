using ParsMedeQ.Application.Services.UserContextAccessorServices;

namespace ParsMedeQ.Presentation.Services.ApplicationServices.UserContextAccessorServices;
sealed class UserContextAccessor : IUserContextAccessor
{
    private static readonly AsyncLocal<UserContext?> _current = new();
    public UserContext? Current
    {
        get => _current.Value ?? UserContext.Guest;
        set => _current.Value = value;
    }
    public UserContext GetCurrent() => Current ?? UserContext.Guest;
    public bool IsGuest()
    {
        var current = this.GetCurrent();
        return current.Equals(UserContext.Guest) || current.UserId.Equals(default) || current.UserId.Equals(0);
    }
}
