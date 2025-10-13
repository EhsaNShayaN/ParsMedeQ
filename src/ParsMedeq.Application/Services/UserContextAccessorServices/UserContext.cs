namespace ParsMedeQ.Application.Services.UserContextAccessorServices;

public sealed record UserContext(int UserId)
{
    public readonly static UserContext Guest = new(0);

    public static UserContext Create(int id) => new(id);
}
