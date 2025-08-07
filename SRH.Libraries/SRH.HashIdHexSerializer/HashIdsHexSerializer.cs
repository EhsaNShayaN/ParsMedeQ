using HashidsNet;

namespace SRH.HashIdHexSerializer;

public sealed class HashIdsHexSerializer
{
    private readonly Hashids _hashids;

    public HashIdsHexSerializer(Hashids hashids)
    {
        _hashids = hashids;
    }
    public string Serialize(Func<string> serializer)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(serializer.Invoke());
        var hexed = BitConverter.ToString(bytes).Replace("-", "");
        return _hashids.EncodeHex(hexed);
    }
    public string Serialize(string str)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(str);
        var hexed = BitConverter.ToString(bytes).Replace("-", "");
        return _hashids.EncodeHex(hexed);
    }

    public T Deserialize<T>(string value, Func<string, T> func)
    {
        var span = _hashids.DecodeHex(value).AsSpan();
        var l = span.Length;
        List<byte> array = new();
        for (int i = 0; i < l / 2; i++)
        {
            var s = span.Slice(i * 2, 2);
            array.Add(Convert.ToByte(s.ToString(), 16));
        }
        var str = System.Text.Encoding.UTF8.GetString(array.ToArray());
        return func(str);
    }
}
