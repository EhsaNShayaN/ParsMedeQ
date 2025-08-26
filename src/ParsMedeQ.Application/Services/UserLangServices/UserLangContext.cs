namespace ParsMedeQ.Application.Services.UserLangServices;

public sealed record UserLangContext(string Lang)
{
    public readonly static UserLangContext Empty = new UserLangContext("fa");
}
