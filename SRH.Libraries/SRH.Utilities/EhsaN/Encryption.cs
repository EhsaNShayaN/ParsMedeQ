using System.Security.Cryptography;
using System.Text;

namespace SRH.Utilities.EhsaN;

public class Encryption
{
    public static string Encrypt(string toEncrypt, string SecurityKey, bool useHashing)
    {
        byte[] keyArray;
        byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

        //AppSettingsReader settingsReader = new AppSettingsReader();
        //// Get the key from config file
        //string key = (string)settingsReader.GetValue(SecurityKey, typeof(String));
        string key = SecurityKey;
        //System.Windows.Forms.MessageBox.Show(key);
        //If hashing use get hashcode regards to your key
        if (useHashing)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
            //Always release the resources and flush data
            // of the Cryptographic service provide. Best Practice

            hashmd5.Clear();
        }
        else
            keyArray = Encoding.UTF8.GetBytes(key);

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        //set the secret key for the tripleDES algorithm
        tdes.Key = keyArray;
        //mode of operation. there are other 4 modes.
        //We choose ECB(Electronic code Book)
        tdes.Mode = CipherMode.ECB;
        //padding mode(if any extra byte added)

        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateEncryptor();
        //transform the specified region of bytes array to resultArray
        byte[] resultArray =
          cTransform.TransformFinalBlock(toEncryptArray, 0,
          toEncryptArray.Length);
        //Release resources held by TripleDes Encryptor
        tdes.Clear();
        //Return the encrypted data into unreadable string format
        return Convert.ToBase64String(resultArray, 0, resultArray.Length)
                .Replace('+', '_')
                .Replace('/', '-')
                .Replace('=', '.'); ;
    }
    static string Decrypt(string cipherString, string SecurityKey, bool useHashing)
    {
        byte[] keyArray;
        //get the byte code of the string

        byte[] toEncryptArray = Convert.FromBase64String(cipherString
                .Replace('_', '+')
                .Replace('-', '/')
                .Replace('.', '='));

        //AppSettingsReader settingsReader =
        ////Get your key from config file to open the lock!
        //string key = (string)settingsReader.GetValue(SecurityKey, typeof(String));
        string key = SecurityKey;

        if (useHashing)
        {
            //if hashing was used get the hash code with regards to your key
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
            //release any resource held by the MD5CryptoServiceProvider

            hashmd5.Clear();
        }
        else
        {
            //if hashing was not implemented get the byte code of the key
            keyArray = Encoding.UTF8.GetBytes(key);
        }

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        //set the secret key for the tripleDES algorithm
        tdes.Key = keyArray;
        //mode of operation. there are other 4 modes. 
        //We choose ECB(Electronic code Book)

        tdes.Mode = CipherMode.ECB;
        //padding mode(if any extra byte added)
        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateDecryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(
                             toEncryptArray, 0, toEncryptArray.Length);
        //Release resources held by TripleDes Encryptor                
        tdes.Clear();
        //return the Clear decrypted TEXT
        return Encoding.UTF8.GetString(resultArray);
    }
    public static string Encrypt(string toEncrypt)
    {
        return Encrypt(toEncrypt, "zigzag", true);
    }
    public static string Decrypt(string cipherString)
    {
        return Decrypt(cipherString, "zigzag", true);
    }
    public static string Encrypt2(string clearText)
    {
        try
        {
            string EncryptionKey = "zigzag";
            byte[] clearBytes = Encoding.UTF8.GetBytes(clearText);
            using Aes encryptor = Aes.Create();
            Rfc2898DeriveBytes pdb = new(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using MemoryStream ms = new();
            using (CryptoStream cs = new(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(clearBytes, 0, clearBytes.Length);
                cs.Close();
            }
            //clearText = Convert.ToBase64String(ms.ToArray());
            //clearText = clearText.UrlEncode();
            char[] padding = { '=' };
            clearText = Convert.ToBase64String(ms.ToArray()).TrimEnd(padding).Replace('+', '-').Replace('/', '_');

        }
        catch
        {
            clearText = null;
        }
        return clearText;
    }
    public static string Decrypt2(string cipherText)
    {
        try
        {
            //cipherText = cipherText.UrlDecode();
            cipherText = cipherText.Replace('_', '/').Replace('-', '+');
            switch (cipherText.Length % 4)
            {
                case 2: cipherText += "=="; break;
                case 3: cipherText += "="; break;
            }
            string EncryptionKey = "zigzag";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using Aes encryptor = Aes.Create();
            Rfc2898DeriveBytes pdb = new(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using MemoryStream ms = new();
            using (CryptoStream cs = new(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(cipherBytes, 0, cipherBytes.Length);
                cs.Close();
            }
            cipherText = Encoding.UTF8.GetString(ms.ToArray());
        }
        catch
        {
            cipherText = null;
        }
        return cipherText;
    }
    public static string GenerateMD5Hash(string str)
    {
        if (string.IsNullOrEmpty(str)) return null;
        var md5 = MD5.Create();
        byte[] inputBytes = Encoding.ASCII.GetBytes(str);
        byte[] hash = md5.ComputeHash(inputBytes);

        // step 2, convert byte array to hex string
        var sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }
        return sb.ToString();
    }
}