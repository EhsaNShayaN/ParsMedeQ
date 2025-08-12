namespace ParsMedeq.Domain.DomainServices.EncryptionService;
public interface IEncryptionService
{
    ValueTask<PrimitiveResult<EncryptionResult>> Encrypt(byte[] src);
    ValueTask<PrimitiveResult<EncryptionResult>> Encrypt(string src);
    ValueTask<PrimitiveResult<string>> EncryptValue(string src);
    ValueTask<PrimitiveResult<byte[]>> Decrypt(string src, string id);
    ValueTask<PrimitiveResult<string>> Decrypt(string src);
    ValueTask<PrimitiveResult<string>> Decrypt(byte[] src);

    string DbEncrypt(string src);
    string DbDecrypt(string src);
}

public record EncryptionResult(string EncryptedValue, string EncryptKeyId)
{
    public string GetValue() => $"{EncryptedValue}|{EncryptKeyId}";
}