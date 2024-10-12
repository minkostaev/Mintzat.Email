﻿namespace Mintzat.Email.ResendCom;

using Mintzat.Email.Services;
using System.Text;
using System.Text.Json;

public class ResendSender
{
    private const string _emailsUri = $"https://api.resend.com/emails";
    private readonly HttpClient _emailClient;
    private readonly string _defaultSender;

    /// <summary>
    /// Constructor preparing sending emails with resend.com
    /// </summary>
    /// <param name="apiKey">API Key from resend.com</param>
    /// <param name="defaultSender">email from your domain that is added to resend.com DNS</param>
    public ResendSender(string apiKey, string defaultSender = "")
    {
        _emailClient = new HttpClient();
        _emailClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        _defaultSender = defaultSender;
    }

    /// <summary>
    /// Sending email using the resend api
    /// </summary>
    /// <param name="sender">Email from</param>
    /// <param name="recipients">Email to recipients</param>
    /// <param name="topic">Email subject</param>
    /// <param name="content">Email's html</param>
    /// <param name="replyTo">Email reply to</param>
    /// <param name="ccEmails">Email cc recipients</param>
    /// <param name="bccEmails">Email bcc recipients</param>
    /// <param name="attachedFiles">Email's attached files</param>
    /// <returns>Success state and error message</returns>
    public async Task<(bool, string)> SendEmail(string sender, string[] recipients, string topic, string content,
        string? replyTo = null, string[]? ccEmails = null, string[]? bccEmails = null,
        Dictionary<string, string>? attachedFiles = null)
    {
        List<object> attFiles = [];
        if (attachedFiles != null)
        {
            foreach (var file in attachedFiles)
            {
                var attachedFile = new
                {
                    filename = file.Key,//file name and extension
                    content = file.Value,//base64 encoded content
                    ///type = "application/pdf"
                };
                attFiles.Add(attachedFile);
            }
        }

        #region Validation (for preventing failure)
        // topic and content (prevent empty)
        if (string.IsNullOrEmpty(topic))
            topic = " ";
        if (string.IsNullOrEmpty(content))
            content = " ";
        // sender email (valid and domain dependant)
        if (!Validation.IsValidEmail(sender) && Validation.IsValidEmail(_defaultSender))
            sender = _defaultSender;
        string domainSender = Validation.GetDomainFromEmail(sender);
        string domainDefault = Validation.GetDomainFromEmail(_defaultSender);
        if (domainSender != domainDefault && Validation.IsValidEmail(_defaultSender))
            sender = _defaultSender;
        // replyTo (ignore if invalid email)
        if (!Validation.IsValidEmail(replyTo))
            replyTo = null;
        // Emails collections
        if (recipients != null)
            recipients = Validation.ValidateEmails(recipients)!;
        // to do null case
        ccEmails = Validation.ValidateEmails(ccEmails);
        bccEmails = Validation.ValidateEmails(bccEmails);
        #endregion

        var emailContent = new
        {
            //from = "Acme <onboarding@resend.dev>", to do add name
            from = sender,
            to = recipients,
            subject = topic,
            html = content,
            ///text = "This is the plain text version of the email.",
            reply_to = replyTo,
            cc = ccEmails,
            bcc = bccEmails,
            attachments = attFiles,
            ///attachments = new[] {
            ///    new {
            ///        filename = "example.pdf",
            ///        content = "base64encodedcontent",
            ///        type = "application/pdf"
            ///    }
            ///},
            
            ///priority = "high",
            ///idempotency_key = "unique-idempotency-key",
            ///headers = new
            ///{
            ///    "X-Custom-Header": "Custom Value"
            ///},
            ///tags = new
            ///{
            ///    category = "newsletter",
            ///    campaign_id = "summer-sale"
            ///}
        };
        return await SendEmail(emailContent);
    }

    private async Task<(bool, string)> SendEmail(object emailContent)
    {
        StringContent? content;
        try
        {
            string json = JsonSerializer.Serialize(emailContent);
            content = new StringContent(json, Encoding.UTF8, "application/json");
        }
        catch(Exception ex)
        {
            return (false, ex.Message);
        }

        HttpResponseMessage? response;
        string responseBody;
        try
        {
            response = await _emailClient.PostAsync(_emailsUri, content);
            responseBody = await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }

        return (response.IsSuccessStatusCode, responseBody);
    }

}