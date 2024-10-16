namespace Mintzat.Email.Tests;

using Mintzat.Email.Services;

public class ValidationTests
{
    [Test]
    public void GetAttachedFileType_Success()
    {
        string? caseNull = Validation.GetAttachedFileType(string.Empty);
        string? caseDefault = Validation.GetAttachedFileType("default");

        string? caseTxt = Validation.GetAttachedFileType(".txt");
        string? caseHtml = Validation.GetAttachedFileType(".html");
        string? caseCss = Validation.GetAttachedFileType(".css");
        string? caseJs = Validation.GetAttachedFileType(".js");
        string? caseCsv = Validation.GetAttachedFileType(".csv");

        //".jpg" => "image/jpeg",
        //".jpeg" => "image/jpeg",
        //".png" => "image/png",
        //".gif" => "image/gif",
        //".svg" => "image/svg+xml",
        //".webp" => "image/webp",

        //".mp3" => "audio/mpeg",
        //".wav" => "audio/wav",
        //".ogg" => "audio/ogg",
        //".aac" => "audio/aac",

        //".mp4" => "video/mp4",
        //".webm" => "video/webm",
        //".ogv" => "video/ogg",
        //".mpeg" => "video/mpeg",

        //".pdf" => "application/pdf",
        //".json" => "application/json",
        //".xml" => "application/xml",
        //".xls" => "application/vnd.ms-excel",
        //".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //".doc" => "application/msword",
        //".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
        //".zip" => "application/zip",

        //".otf" => "font/otf",
        //".ttf" => "font/ttf",
        //".woff" => "font/woff",

        Assert.Multiple(() =>
        {
            Assert.That(caseNull, Is.Null);
            Assert.That(caseDefault, Is.EqualTo("application/octet-stream"));
            Assert.That(caseTxt, Is.EqualTo("text/plain"));
            Assert.That(caseHtml, Is.EqualTo("text/html"));
            Assert.That(caseCss, Is.EqualTo("text/css"));
            Assert.That(caseJs, Is.EqualTo("text/javascript"));
            Assert.That(caseCsv, Is.EqualTo("text/csv"));
        });
    }
}