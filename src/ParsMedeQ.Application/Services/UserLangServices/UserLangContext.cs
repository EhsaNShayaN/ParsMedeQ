using ParsMedeQ.Domain;

namespace ParsMedeQ.Application.Services.UserLangServices;

public sealed record UserLangContext(string Lang)
{
    public readonly static UserLangContext Empty = new UserLangContext(Constants.LangCode_Farsi.ToLower());
}
