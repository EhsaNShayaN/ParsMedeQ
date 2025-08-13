using System.Security.Cryptography;
using System.Text;

namespace ParsMedeQ.Domain.Helpers;

public static class PasswordHelper
{
    readonly static Random _random = new Random();
    readonly static PrimitiveError _wrongPasswordError = PrimitiveError.Create("", "Wrong password");


    public const int SALT_KEY_SIZE = 64;
    const int iterations = 350000;
    static HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

    public static string HashPassword(string password, out byte[] salt)
    {
        salt = RandomNumberGenerator.GetBytes(SALT_KEY_SIZE);
        return CreatePbkdf2(password, salt);
    }

    public static PrimitiveResult VerifyPassword(string password, string hash, byte[] salt)
    {
        var hashToCompare = CreatePbkdf2(password, salt);
        var result = CryptographicOperations.FixedTimeEquals(Convert.FromHexString(hashToCompare), Convert.FromHexString(hash));
        return result
            ? PrimitiveResult.Success()
            : PrimitiveResult.Failure(_wrongPasswordError);

    }

    public static PrimitiveResult<GeneratePasswordResult> HashAndSaltPassword(string password)
    {
        var hashedPassword = HashPassword(password, out var salt);
        return PrimitiveResult.Success(new GeneratePasswordResult(
            hashedPassword,
            Convert.ToBase64String(salt)));
    }

    public static PrimitiveResult<string> GenerateRandomPassword(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[_random.Next(s.Length)]).ToArray());

    }

    private static string CreatePbkdf2(string password, byte[] salt) =>
        Convert.ToHexString(
            Rfc2898DeriveBytes.Pbkdf2(
           Encoding.UTF8.GetBytes(password),
           salt,
           iterations,
           hashAlgorithm,
           SALT_KEY_SIZE));
}
public record GeneratePasswordResult(string HashedPassword, string Salt);
