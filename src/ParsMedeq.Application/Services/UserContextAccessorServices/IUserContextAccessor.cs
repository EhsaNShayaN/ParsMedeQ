namespace ParsMedeQ.Application.Services.UserContextAccessorServices;
public interface IUserContextAccessor
{
    UserContext? Current { get; set; }

    UserContext GetCurrent();

    bool IsGuest();
}