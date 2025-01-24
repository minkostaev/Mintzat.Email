namespace Mintzat.Email.Models.TheMachine;

using System.Text.Json.Serialization;

public class Machine
{
    public string? Hash { get; set; }
    public Client? Client { get; set; }
    public Version? Version { get; set; }
    public Culture? Culture { get; set; }
    public Processor? Processor { get; set; }
    public List<Network>? Networks { get; set; }
    public Dictionary<string, string?>? Variables { get; set; }
}
public class MachineMongo : Machine
{
    [JsonPropertyName("_id")]
    public string? Id { get; set; }
}