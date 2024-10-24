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
    public static string? GetAttachedFileType(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            return null;
        string extension = Path.GetExtension(fileName);
        return extension switch
        {
            ".txt" => "text/plain",
            ".html" => "text/html",
            ".css" => "text/css",
            ".js" => "text/javascript",
            ".csv" => "text/csv",

            ".jpg" => "image/jpeg",
            ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".svg" => "image/svg+xml",
            ".webp" => "image/webp",

            ".mp3" => "audio/mpeg",
            ".wav" => "audio/wav",
            ".ogg" => "audio/ogg",
            ".aac" => "audio/aac",

            ".mp4" => "video/mp4",
            ".webm" => "video/webm",
            ".ogv" => "video/ogg",
            ".mpeg" => "video/mpeg",

            ".pdf" => "application/pdf",
            ".json" => "application/json",
            ".xml" => "application/xml",
            ".xls" => "application/vnd.ms-excel",
            ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ".doc" => "application/msword",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".zip" => "application/zip",

            ".otf" => "font/otf",
            ".ttf" => "font/ttf",
            ".woff" => "font/woff",

            _ => "application/octet-stream"
        };
    }
    public static object[]? GetAttachedFiles(Dictionary<string, string>? files)
    {
        List<object>? result = [];
        if (files != null)
        {
            foreach (var file in files)
            {
                var attachedFile = new
                {
                    filename = file.Key,//file name and extension
                    content = file.Value,//base64 encoded content
                    type = GetAttachedFileType(file.Key)
                };
                result.Add(attachedFile);
            }
        }
        if (result.Count == 0)
            return null;
        else
            return [.. result];
    }

}