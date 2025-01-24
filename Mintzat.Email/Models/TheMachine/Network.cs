namespace Mintzat.Email.Models.TheMachine;

using System.Net.NetworkInformation;
using System.Net.Sockets;

public class Network
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Type { get; set; }
    public string? Mac { get; set; }
    public string? Ip { get; set; }
}
public class Networks : List<Network>
{
    public Networks()
    {
        try { AddAllNetworks(); }
        catch (Exception ex) { this.Add(new Network { Description = ex.Message }); }
    }
    private void AddAllNetworks()
    {
        foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (nic.OperationalStatus == OperationalStatus.Up)
            {
                var networkMachine = new Network
                {
                    Id = nic.Id.Replace("{", "").Replace("}", ""),
                    Name = nic.Name,
                    Description = nic.Description,
                    Type = nic.NetworkInterfaceType.ToString()
                };
                var ip = nic.GetIPProperties().UnicastAddresses
                    .FirstOrDefault(x => x.Address.AddressFamily == AddressFamily.InterNetwork);
                if (ip != null)
                    networkMachine.Ip = ip.Address.ToString();
                var address = nic.GetPhysicalAddress();
                var mac = BitConverter.ToString(address.GetAddressBytes());
                networkMachine.Mac = mac;///address.ToString();
                this.Add(networkMachine);
            }
        }
    }
}