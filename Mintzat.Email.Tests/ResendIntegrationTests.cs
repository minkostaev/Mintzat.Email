﻿namespace Mintzat.Email.Tests;

using Microsoft.Extensions.Configuration;
using Mintzat.Email.ResendCom;

public class ResendIntegrationTests
{
    private ResendSender _sender;
    private IConfiguration _configuration;
    private string _senderEmail = "";
    private string _recipientEmail = "";
    private string _ccEmail = "";
    private string _bccEmail = "";

    [SetUp]
    public void Setup()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        string apiKey;
        try
        {
            _configuration = builder.Build();
            apiKey = _configuration["ApiKey"]!;
        }
        catch
        {
            apiKey = Environment.GetEnvironmentVariable("APIKEY")!;
        }
        _sender = new ResendSender(apiKey);
        if (_configuration == null)
        {
            _senderEmail = Environment.GetEnvironmentVariable("SENDER")!;
            _recipientEmail = Environment.GetEnvironmentVariable("RECIPIENTS")!;
            _ccEmail = Environment.GetEnvironmentVariable("CC")!;
            _bccEmail = Environment.GetEnvironmentVariable("BCC")!;
        }
        else
        {
            _senderEmail = _configuration["Sender"]!;
            _recipientEmail = _configuration["Recipients"]!;
            _ccEmail = _configuration["Cc"]!;
            _bccEmail = _configuration["Bcc"]!;
        }
    }

    [Test]
    public async Task SendEmail_Success()
    {
        //string sender = string.Empty;
        //string recipients = string.Empty;
        //string cc = string.Empty;
        //string bcc = string.Empty;
        //if (_configuration == null)
        //{
        //    sender = Environment.GetEnvironmentVariable("SENDER")!;
        //    recipients = Environment.GetEnvironmentVariable("RECIPIENTS")!;
        //    cc = Environment.GetEnvironmentVariable("CC")!;
        //    bcc = Environment.GetEnvironmentVariable("BCC")!;
        //}
        //else
        //{
        //    sender = _configuration["Sender"]!;
        //    recipients = _configuration["Recipients"]!;
        //    cc = _configuration["Cc"]!;
        //    bcc = _configuration["Bcc"]!;
        //}

        Dictionary<string, string> keyValuePairs = [];
        keyValuePairs.Add("pdf.pdf", "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NiA1IDAgUg0vRjEwIDcgMCBSDT4+DT4+DWVuZG9iag01IDAgb2JqDTw8DS9UeXBlIC9Gb250DS9TdWJ0eXBlIC9UeXBlMQ0vTmFtZSAvRjYNL0VuY29kaW5nIC9NYWNSb21hbkVuY29kaW5nDS9CYXNlRm9udCAvVGltZXMtUm9tYW4NPj4NZW5kb2JqDTYgMCBvYmoNPDwNL1R5cGUgL0ZvbnQNL1N1YnR5cGUgL1R5cGUxDS9OYW1lIC9GOA0vRmlyc3RDaGFyIDANL0xhc3RDaGFyIDI1NQ0vV2lkdGhzIFsNIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDANIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDANIDIyNCAyMjQgMzIwIDQ0OCA0NDggNzA0IDYwOCAxNzYgMzIwIDMyMCAyNzIgNDQ4IDIyNCAyNDAgMjI0IDM1Mg0gNDQ4IDQ0OCA0NDggNDQ4IDQ0OCA0NDggNDQ4IDQ0OCA0NDggNDQ4IDIyNCAyMjQgNDQ4IDQ0OCA0NDggMzM2DSA1NzYgNTI4IDUxMiA1MjggNjA4IDQ4MCA0NDggNTc2IDYyNCAyODggMzIwIDU5MiA0MzIgNjg4IDU3NiA2MDgNIDQ4MCA2MDggNTI4IDQxNiA0ODAgNTYwIDUxMiA3NTIgNTYwIDU0NCA0OTYgMjI0IDQzMiAyMjQgNDQ4IDQwMA0gMzIwIDQxNiA0ODAgNDAwIDQ4MCA0MTYgMjg4IDQzMiA1MjggMjU2IDI0MCA0ODAgMjU2IDc1MiA1MjggNDgwDSA1MTIgNDgwIDM2OCAzNjggMjcyIDQ4MCA0MzIgNjU2IDQ5NiA0NDggMzg0IDIyNCA0NDggMjI0IDQ0OCAyMjQNIDUyOCA1MjggNTI4IDQ4MCA1NzYgNjA4IDU2MCA0MTYgNDE2IDQxNiA0MTYgNDE2IDQxNiA0MDAgNDE2IDQxNg0gNDE2IDQxNiAyNTYgMjU2IDI1NiAyNTYgNTI4IDQ4MCA0ODAgNDgwIDQ4MCA0ODAgNDgwIDQ4MCA0ODAgNDgwDSA0MzIgMzIwIDQ0OCA0NDggMzIwIDQ5NiA1MjggNTEyIDYwOCA2MDggNjg4IDMyMCAzNTIgMCA3NjggNjA4DSAwIDQ0OCAwIDAgNDQ4IDQ4MCAwIDAgMCAwIDAgMjg4IDI4OCAwIDY0MCA0ODANIDMzNiAyMjQgNDQ4IDAgNDQ4IDAgMCAyNzIgMjcyIDgwMCAwIDUyOCA1MjggNjA4IDgwMCA3MzYNIDQwMCA4MDAgMzUyIDM1MiAyMDggMjA4IDQ0OCAwIDQ0OCA1NDQgMTI4IDQ0OCAxNjAgMTYwIDU2MCA1NjANIDQzMiAyMjQgMjA4IDM1MiAxMDA4IDUyOCA0ODAgNTI4IDQ4MCA0ODAgMjg4IDI4OCAyODggMjg4IDYwOCA2MDgNIDAgNjA4IDU2MCA1NjAgNTYwIDI1NiA0MDAgNDE2IDQxNiAzODQgMjQwIDI1NiAyNTYgMzM2IDI3MiA0MDANXQ0vRW5jb2RpbmcgL01hY1JvbWFuRW5jb2RpbmcNL0Jhc2VGb250IC9HYXJhbW9uZC1Cb2xkDS9Gb250RGVzY3JpcHRvciAxMiAwIFINPj4NZW5kb2JqDTcgMCBvYmoNPDwNL1R5cGUgL0ZvbnQNL1N1YnR5cGUgL1R5cGUxDS9OYW1lIC9GMTANL0ZpcnN0Q2hhciAwDS9MYXN0Q2hhciAyNTUNL1dpZHRocyBbDSAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwDSAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwIDAgMCAwDSAyMDggMTc2IDI3MiA0MDAgNDAwIDYwOCA1NjAgMTYwIDI4OCAyODggMjcyIDQ0OCAyMDggMjU2IDIwOCAzMjANIDQwMCA0MDAgNDAwIDQwMCA0MDAgNDAwIDQwMCA0MDAgNDAwIDQwMCAyMDggMjA4IDQ0OCA0NDggNDQ4IDI0MA0gNTkyIDUxMiA1MTIgNDk2IDU5MiA0NDggNDE2IDU5MiA1OTIgMjU2IDI1NiA1MjggMzg0IDY0MCA1NjAgNjI0DSA0NDggNjI0IDQ2NCAzODQgNDY0IDU2MCA1MTIgNzM2IDUxMiA1MjggNDgwIDE5MiA0MTYgMTkyIDQ0OCA0MDANIDI4OCAzODQgNDQ4IDM2OCA0NDggNDAwIDI0MCA0MTYgNDQ4IDIwOCAxNzYgNDMyIDIwOCA2NTYgNDQ4IDQ0OA0gNDY0IDQ0OCAyNzIgMzIwIDIyNCA0NDggNDAwIDYyNCA0MTYgNDAwIDM2OCAyMDggNDQ4IDIwOCA0NDggMjA4DSA1MTIgNTEyIDQ5NiA0NDggNTYwIDYyNCA1NjAgMzg0IDM4NCAzODQgMzg0IDM4NCAzODQgMzY4IDQwMCA0MDANIDQwMCA0MDAgMjA4IDIwOCAyMDggMjA4IDQ0OCA0NDggNDQ4IDQ0OCA0NDggNDQ4IDQ0OCA0NDggNDQ4IDQ0OA0gMzIwIDMyMCA0MDAgNDAwIDI4OCA0OTYgNDk2IDQ0OCA2MDggNjA4IDY4OCAyODggMzIwIDAgNzUyIDYyNA0gMCA0NDggMCAwIDQwMCA0NDggMCAwIDAgMCAwIDI3MiAyNzIgMCA1OTIgNDQ4DSAyNDAgMTc2IDQ0OCAwIDQwMCAwIDAgMjA4IDIwOCA4MDAgMCA1MTIgNTEyIDYyNCA4MDAgNzA0DSA0MDAgODAwIDMwNCAzMDQgMTc2IDE3NiA0NDggMCA0MDAgNTI4IDEyOCA0MDAgMTI4IDEyOCA0NDggNDQ4DSAzMjAgMjA4IDE3NiAyODggODY0IDUxMiA0NDggNTEyIDQ0OCA0NDggMjU2IDI1NiAyNTYgMjU2IDYyNCA2MjQNIDAgNjI0IDU2MCA1NjAgNTYwIDIwOCAzMzYgMzM2IDMwNCAzMjAgMTc2IDIyNCAyNTYgMzA0IDI0MCAzMzYNXQ0vRW5jb2RpbmcgL01hY1JvbWFuRW5jb2RpbmcNL0Jhc2VGb250IC9HYXJhbW9uZC1MaWdodA0vRm9udERlc2NyaXB0b3IgMTMgMCBSDT4+DWVuZG9iag0xMiAwIG9iag08PA0vVHlwZSAvRm9udERlc2NyaXB0b3INL0FzY2VudCA3MDYNL0NhcEhlaWdodCA2MjMNL0Rlc2NlbnQgLTIyNQ0vRmxhZ3MgMjk0OTYyDS9Gb250QkJveCBbLTEyNCAtMjUwIDEyNDcgODQ1IF0NL0ZvbnROYW1lIC9HYXJhbW9uZC1Cb2xkDS9JdGFsaWNBbmdsZSAwDS9TdGVtViAxNDgNL1hIZWlnaHQgNDU3DT4+DWVuZG9iag0xMyAwIG9iag08PA0vVHlwZSAvRm9udERlc2NyaXB0b3INL0FzY2VudCA3MDYNL0NhcEhlaWdodCA2MjMNL0Rlc2NlbnQgLTIyOQ0vRmxhZ3MgMzI4MTgNL0ZvbnRCQm94IFstMTU2IC0yNTAgMTA2NiA4NDIgXQ0vRm9udE5hbWUgL0dhcmFtb25kLUxpZ2h0DS9JdGFsaWNBbmdsZSAwDS9TdGVtViA2OQ0vWEhlaWdodCA0MzkNPj4NZW5kb2JqDTIgMCBvYmoNPDwNL1R5cGUgL1BhZ2UNL1BhcmVudCA4IDAgUg0vUmVzb3VyY2VzIDQgMCBSDS9Db250ZW50cyAzIDAgUg0vQ3JvcEJveCBbMTIgMTYgNTk5IDc3NiBdDT4+DWVuZG9iag05IDAgb2JqDTw8DS9UeXBlIC9QYWdlDS9QYXJlbnQgOCAwIFINL1Jlc291cmNlcyAxMSAwIFINL0NvbnRlbnRzIDEwIDAgUg0vQ3JvcEJveCBbMTIgMTYgNTk5IDc3NiBdDT4+DWVuZG9iag04IDAgb2JqDTw8DS9UeXBlIC9QYWdlcw0vS2lkcyBbMiAwIFIgOSAwIFIgXQ0vQ291bnQgMg0vTWVkaWFCb3ggWzAgMCA2MTIgNzkyIF0NPj4NZW5kb2JqDTE0IDAgb2JqDTw8DS9UeXBlIC9DYXRhbG9nDS9QYWdlcyA4IDAgUg0+Pg1lbmRvYmoNMTUgMCBvYmoNPDwNL0NyZWF0aW9uRGF0ZSAoRDoxOTk2MTAxMjE1MDczNykNL1Byb2R1Y2VyIChBY3JvYmF0IERpc3RpbGxlciAyLjAgZm9yIE1hY2ludG9zaCkNPj4NZW5kb2JqDXhyZWYNMCAxNg0wMDAwMDAwMDAwIDY1NTM1IGYgDTAwMDAwMDAwMTYgMDAwMDAgbiANMDAwMDAwNTQ3MCAwMDAwMCBuIA0wMDAwMDAwMTcyIDAwMDAwIG4gDTAwMDAwMDIwNDkgMDAwMDAgbiANMDAwMDAwMjczMiAwMDAwMCBuIA0wMDAwMDAyODQyIDAwMDAwIG4gDTAwMDAwMDM5NjEgMDAwMDAgbiANMDAwMDAwNTY4NCAwMDAwMCBuIA0wMDAwMDA1NTc2IDAwMDAwIG4gDTAwMDAwMDIxNjkgMDAwMDAgbiANMDAwMDAwMjYyMSAwMDAwMCBuIA0wMDAwMDA1MDgxIDAwMDAwIG4gDTAwMDAwMDUyNzYgMDAwMDAgbiANMDAwMDAwNTc3MyAwMDAwMCBuIA0wMDAwMDA1ODIzIDAwMDAwIG4gDXRyYWlsZXINPDwNL1NpemUgMTYNL1Jvb3QgMTQgMCBSDS9JbmZvIDE1IDAgUg0vSUQgWzw1OGVlOGU3MDUyOWU3ODRlYjYzM2E3ZTgwMDI3NmQ2YT48NThlZThlNzA1MjllNzg0ZWI2MzNhN2U4MDAyNzZkNmE+XQ0+Pg1zdGFydHhyZWYNNTkyNg0lJUVPRg0=");
        keyValuePairs.Add("txt.txt", "dHh0IGRvYw==");

        var result = await _sender.SendEmail(_senderEmail, [_recipientEmail],
            "Test Email from Resend",
            "<h1>Hello!</h1><p>This is a test email sent using Resend API.</p>",
            null, [_ccEmail], [_bccEmail], keyValuePairs);

        var result2 = await _sender.SendEmail(_senderEmail, [_recipientEmail],
            "Test Email from Resend",
            "<h1>Hello!</h1><p>This is a test email sent using Resend API.</p>",
            null, [_bccEmail], [_ccEmail]);

        Assert.Multiple(() =>
        {
            Assert.That(result.Item1, Is.True);
            Assert.That(result2.Item1, Is.True);
        });
    }

}