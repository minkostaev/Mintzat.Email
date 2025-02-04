namespace Mintzat.Email.Tests;

using Mintzat.Email.Services;

public class CryptographyAlgorithmTests
{
    private CryptographyAlgorithm _algorithm;

    [SetUp]
    public void Setup()
    {
        _algorithm = new CryptographyAlgorithm();
    }

    [Test]
    public void EncryptDecrypt()
    {
        string textToEncrypt = "text";
        string key = "key";
        string encrypted = _algorithm.EncryptPlainTextToCipherText(textToEncrypt, key, out string output);
        string result = _algorithm.DecryptCipherTextToPlainText(encrypted, key, output);
        Assert.That(textToEncrypt, Is.EqualTo(result));
    }

    [Test]
    public void Hash()
    {
        string textToEncrypt = "text";
        string salt = "salt";
        string result = CryptographyAlgorithm.HashString(textToEncrypt, salt);
        Assert.That(result, Is.EqualTo("3353E16497AD272FEA4382119FF2801E54F0A4CF2057F4E32D00317BDA5126C3"));
    }
    [Test]
    public void HashEmpty()
    {
        string result = CryptographyAlgorithm.HashString(string.Empty);
        Assert.That(result, Is.Empty);
    }

}