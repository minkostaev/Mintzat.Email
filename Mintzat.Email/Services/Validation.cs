namespace Mintzat.Email.Services;

using System.Text.RegularExpressions;

public static class Validation
{
    public static bool IsValidEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }
    public static string GetDomainFromEmail(string email)
    {
        if (!IsValidEmail(email))
            return string.Empty;
        return email.Split('@')[1];
    }
    public static string[]? ValidateEmails(string[]? emails)
    {
        if (emails == null)
            return null;
        return (from string email in emails
                where IsValidEmail(email)
                select email).ToArray();
    }
}