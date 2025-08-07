using System.IO.Compression;
using System.Text;

namespace SRH.Utilities.Helpers;
public static class GzipHelper
{
    public static byte[]? Compress(byte[] bytes)
    {
        using (var msi = new MemoryStream(bytes))
        using (var mso = new MemoryStream())
        {
            using (var gs = new GZipStream(mso, CompressionMode.Compress))
            {
                msi.CopyTo(gs);
            }
            return mso.ToArray();
        }
    }
    public static byte[]? Compress(string data)
    {
        if (string.IsNullOrEmpty(data))
            return null;

        var bytes = Encoding.UTF8.GetBytes(data);
        return Compress(bytes);

    }
    public static byte[] Decompress(byte[] compressedData)
    {
        if (compressedData == null || compressedData.Length == 0)
            return Array.Empty<byte>();

        using (var msi = new MemoryStream(compressedData))
        using (var mso = new MemoryStream())
        {
            using (var gs = new GZipStream(msi, CompressionMode.Decompress))
            {
                gs.CopyTo(mso);
            }
            return mso.ToArray();
        }
    }
}