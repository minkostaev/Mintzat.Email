namespace Mintzat.Email.Services;

using System.Security.Cryptography;
using System.Text;

public class CryptographyAlgorithm
{
    public CryptographyAlgorithm()
    {
        _cipherMode = CipherMode.CBC;
    }

    #region public

    public static string HashString(string text, string salt = "")
    {
        if (string.IsNullOrEmpty(text))
        {
            return string.Empty;
        }
        // Uses SHA256 to create the hash
        using (var sha = SHA256.Create())
        {
            // Convert the string to a byte array first, to be processed
            byte[] textBytes = Encoding.UTF8.GetBytes(text + salt);
            byte[] hashBytes = sha.ComputeHash(textBytes);

            // Convert back to a string, removing the '-' that BitConverter adds
            string hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
            return hash;
        }
    }

    public string EncryptPlainTextToCipherText(string plainText, string securityKey, out string vectorAlgorithm)
    {
        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(plainText);
        var encryption = Encrypt(toEncryptArray, CreateKey(securityKey), out byte[] iv);
        vectorAlgorithm = Convert.ToBase64String(iv, 0, iv.Length);
        return Convert.ToBase64String(encryption, 0, encryption.Length);
    }

    public string DecryptCipherTextToPlainText(string cipherText, string securityKey, string vectorAlgorithm)
    {
        byte[] toEncryptArray = Convert.FromBase64String(cipherText);
        byte[] iv = Convert.FromBase64String(vectorAlgorithm);
        var newDecrypt = Decrypt(toEncryptArray, CreateKey(securityKey), iv!);
        return UTF8Encoding.UTF8.GetString(newDecrypt);
    }

    #endregion

    #region private

    private static byte[] CreateKey(string key)
    {
        var objMD5CryptoService = MD5.Create();
        //Getting the bytes from the Security Key and Passing it to compute the Corresponding Hash Value.
        byte[] securityKeyArray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
        //De-allocatinng the memory after doing the Job.
        objMD5CryptoService.Clear();
        return securityKeyArray;
    }

    // https://www.meziantou.net/cryptography-in-dotnet.htm
    private readonly CipherMode _cipherMode;
    private byte[] Encrypt(byte[] data, byte[] key, out byte[] iv)
    {
        using var aes = Aes.Create();
        // You should adjust the mode depending on what you want to encrypt.
        // However, some mode may be weak or require additional security steps such as CBC:
        // https://learn.microsoft.com/en-us/dotnet/standard/security/vulnerabilities-cbc-mode?WT.mc_id=DT-MVP-5003978
        aes.Mode = _cipherMode;
        aes.Key = key;
        aes.GenerateIV();// You must use a new IV for each encryption for security purpose
        using (var cryptoTransform = aes.CreateEncryptor())
        {
            iv = aes.IV;
            return cryptoTransform.TransformFinalBlock(data, 0, data.Length);
        }
    }
    private byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
    {
        using var aes = Aes.Create();
        aes.Mode = _cipherMode;
        aes.Key = key;
        aes.IV = iv;
        using (var cryptoTransform = aes.CreateDecryptor())
        {
            return cryptoTransform.TransformFinalBlock(data, 0, data.Length);
        }
    }

    #endregion

}