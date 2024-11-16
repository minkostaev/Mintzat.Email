namespace Mintzat.Email.ResendCom;

public interface IResendSender
{
    Task<(bool, string)> SendEmail(string senderEmail, string[] recipients, string topic, string content,
         string? replyTo = null, string[]? ccEmails = null, string[]? bccEmails = null,
         Dictionary<string, string>? attachedFiles = null, string senderName = "");
}