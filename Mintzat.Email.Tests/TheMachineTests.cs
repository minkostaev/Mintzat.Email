namespace Mintzat.Email.Tests;

using Mintzat.Email.Models.TheMachine;

public class TheMachineTests
{
    [Test]
    public void TheMachine_Success()
    {
        TheMachine theMachine = new();
        Client client = new();
        Culture culture = new();
        Machine machine = new();
        Network network = new();
        Processor processor = new();
        Variables variables = [];
        Version version = new();

        Assert.Multiple(() =>
        {
            Assert.That(theMachine, Is.Not.Null);
            Assert.That(client, Is.Not.Null);
            Assert.That(culture, Is.Not.Null);
            Assert.That(machine, Is.Not.Null);
            Assert.That(network, Is.Not.Null);
            Assert.That(processor, Is.Not.Null);
            Assert.That(variables, Is.Not.Null);
            Assert.That(version, Is.Not.Null);
        });
    }

    [Test]
    public void MachineMongo_Success()
    {
        MachineMongo machineMongo = new()
        {
            Id = "machineMongo.Id"
        };

        Assert.Multiple(() =>
        {
            Assert.That(machineMongo.Id, Is.Not.Null);
            Assert.That(machineMongo.Hash, Is.Null);
            Assert.That(machineMongo.Networks, Is.Null);
            Assert.That(machineMongo.Variables, Is.Null);
        });
    }

    [Test]
    public void MachineNetworks_Success()
    {
        Network network = new();
        
        Assert.Multiple(() =>
        {
            Assert.That(network.Id, Is.Null);
            Assert.That(network.Name, Is.Null);
            Assert.That(network.Description, Is.Null);
            Assert.That(network.Type, Is.Null);
            Assert.That(network.Mac, Is.Null);
            Assert.That(network.Ip, Is.Null);
        });
    }

}