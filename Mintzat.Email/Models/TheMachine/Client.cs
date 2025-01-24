namespace Mintzat.Email.Models.TheMachine;

public class Client
{
    public Client() { Init(); }
    public Client(bool initialize) { Init(initialize); }
    private void Init(bool initialize = false)
    {
        if (initialize)
        {
            try
            {
                User = Environment.UserName;
                Machine = Environment.MachineName;
                Domain = Environment.UserDomainName;
                Path = Environment.ProcessPath;//CommandLine
                ///CurrentDirectory = Environment.CurrentDirectory;
            }
            catch (Exception ex) { User = ex.Message; }
        }
    }

    public string? User { get; set; }
    public string? Machine { get; set; }
    public string? Domain { get; set; }
    public string? Path { get; set; }
}