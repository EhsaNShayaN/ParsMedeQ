namespace EShop.Application.Services.UserContextAccessorServices;

public readonly record struct UserTokenInfo(
    string Token,
    string Fullname,
    string Mobile);