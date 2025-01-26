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

}