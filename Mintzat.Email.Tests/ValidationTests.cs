﻿namespace Mintzat.Email.Tests;

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

        string? caseJpg = Validation.GetAttachedFileType(".jpg");
        string? caseJpeg = Validation.GetAttachedFileType(".jpeg");
        string? casePng = Validation.GetAttachedFileType(".png");
        string? caseGif = Validation.GetAttachedFileType(".gif");
        string? caseSvg = Validation.GetAttachedFileType(".svg");
        string? caseWebp = Validation.GetAttachedFileType(".webp");

        string? caseMp3 = Validation.GetAttachedFileType(".mp3");
        string? caseWav = Validation.GetAttachedFileType(".wav");
        string? caseOgg = Validation.GetAttachedFileType(".ogg");
        string? caseAac = Validation.GetAttachedFileType(".aac");

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
            Assert.That(caseJpg, Is.EqualTo("image/jpeg"));
            Assert.That(caseJpeg, Is.EqualTo("image/jpeg"));
            Assert.That(casePng, Is.EqualTo("image/png"));
            Assert.That(caseGif, Is.EqualTo("image/gif"));
            Assert.That(caseSvg, Is.EqualTo("image/svg+xml"));
            Assert.That(caseWebp, Is.EqualTo("image/webp"));
            Assert.That(caseMp3, Is.EqualTo("audio/mpeg"));
            Assert.That(caseWav, Is.EqualTo("audio/wav"));
            Assert.That(caseOgg, Is.EqualTo("audio/ogg"));
            Assert.That(caseAac, Is.EqualTo("audio/aac"));
        });
    }
}