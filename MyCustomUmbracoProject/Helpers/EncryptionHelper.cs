using System.Security.Cryptography;
using System.Text;
using MyCustomUmbracoProject.Settings;

namespace MyCustomUmbracoProject.Helpers;

public static class EncryptionHelper
{
    private static byte[] Key;
    private static byte[] IV;

    public static void Initialize(EncryptionSettings settings)
    {
        Key = Encoding.UTF8.GetBytes(settings.Key);
        IV = Encoding.UTF8.GetBytes(settings.IV);
    }
    
    public static string Encrypt(string plainText)
    {
        using var aes = Aes.Create();
        aes.Key = Key;
        aes.IV = IV;

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using var memoryStream = new MemoryStream();
        using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        using var writer = new StreamWriter(cryptoStream);
        
        writer.Write(plainText);
        writer.Flush();
        cryptoStream.FlushFinalBlock();
        return Convert.ToBase64String(memoryStream.ToArray());
    }

    public static string Decrypt(string cipherText)
    {
        using var aes = Aes.Create();
        aes.Key = Key;
        aes.IV = IV;

        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using var memoryStream = new MemoryStream(Convert.FromBase64String(cipherText));
        using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        using var reader = new StreamReader(cryptoStream);
        return reader.ReadToEnd();
    }
}
