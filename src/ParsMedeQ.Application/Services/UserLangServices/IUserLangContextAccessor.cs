namespace ParsMedeQ.Application.Services.UserLangServices;

public interface IUserLangContextAccessor
{
    UserLangContext? Current { get; set; }
    string GetCurrentLang();
}
