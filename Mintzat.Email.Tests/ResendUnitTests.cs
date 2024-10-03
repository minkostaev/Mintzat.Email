namespace Mintzat.Email.Tests;

using Mintzat.Email.ResendCom;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Reflection;

public class ResendUnitTests
{
    private ResendSender _sender;

    [SetUp]
    public void Setup()
    {
        _sender = new ResendSender("apiKey");

        var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("Response content")
        };
        mockHandler// Mock the PostAsync call
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            ).ReturnsAsync(responseMessage);
        var httpClient = new HttpClient(mockHandler.Object);

        Type type = typeof(ResendSender);
        FieldInfo? fieldInfo = type.GetField("_emailClient", BindingFlags.NonPublic | BindingFlags.Instance);
        fieldInfo?.SetValue(_sender, httpClient);
    }

    [Test]
    public async Task SendEmail_Success()
    {
        var result = await _sender.SendEmail("no-reply@abv.com",
            ["recipients@mail.com"],
            "Test Email from Resend",
            "<h1>Hello!</h1><p>This is a test email sent using Resend API.</p>",
            null, ["cc@mail.com"], ["bcc@mail.com"]);

        Assert.That(result.Item1, Is.True);
    }

    [Test]
    public async Task SendEmail_Client_Crash()
    {
        Type type = typeof(ResendSender);
        FieldInfo? fieldInfo = type.GetField("_emailClient", BindingFlags.NonPublic | BindingFlags.Instance);
        fieldInfo?.SetValue(_sender, null);

        var result = await _sender.SendEmail("no-reply@abv.com",
            ["recipients@mail.com"],
            "Test Email from Resend",
            "<h1>Hello!</h1><p>This is a test email sent using Resend API.</p>",
            null, ["cc@mail.com"], ["bcc@mail.com"]);

        Assert.That(result.Item1, Is.False);
    }

    [Test]
    public async Task SendEmail_Json_Crash()
    {
        Type type = typeof(ResendSender);
        MethodInfo? info = type.GetMethod("SendEmail", BindingFlags.NonPublic | BindingFlags.Instance);
        var result = await (Task<(bool, string)>)info?.Invoke(_sender, [new Action(() => { })]);

        Assert.That(result.Item1, Is.False);
    }


}