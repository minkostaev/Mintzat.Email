namespace Mintzat.Email.Models.TheMachine;

using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

public class TheMachine : Machine
{
    public TheMachine()
    {
        Client = new Client(true);
        Culture = new Culture(true);
        Processor = new Processor(true);
        Version = new Version(true);
        Networks = new Networks();
        Variables = new Variables();
        AddHash();
    }
    private void AddHash()
    {
        try
        {
            var clientJson = JsonSerializer.Serialize(Client);
            var cultureJson = JsonSerializer.Serialize(Culture);
            var processorJson = JsonSerializer.Serialize(Processor);
            var versionJson = JsonSerializer.Serialize(Version);
            Hash = HashString(clientJson + cultureJson + processorJson + versionJson);
        }
        catch (Exception ex) { Hash = ex.Message; }
    }

    public static string HashString(string text, string salt = "")
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;
        byte[] textBytes = Encoding.UTF8.GetBytes(text + salt);
        byte[] hashBytes = SHA256.HashData(textBytes);
        string hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
        return hash;
    }

}