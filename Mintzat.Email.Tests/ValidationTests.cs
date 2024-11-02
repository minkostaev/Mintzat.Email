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

        string? caseMp4 = Validation.GetAttachedFileType(".mp4");
        string? caseWebm = Validation.GetAttachedFileType(".webm");
        string? caseOgv = Validation.GetAttachedFileType(".ogv");
        string? caseMpeg = Validation.GetAttachedFileType(".mpeg");

        string? casePdf = Validation.GetAttachedFileType(".pdf");
        string? caseJson = Validation.GetAttachedFileType(".json");
        string? caseXml = Validation.GetAttachedFileType(".xml");
        string? caseXls = Validation.GetAttachedFileType(".xls");
        string? caseXlsx = Validation.GetAttachedFileType(".xlsx");
        string? caseDoc = Validation.GetAttachedFileType(".doc");
        string? caseDocx = Validation.GetAttachedFileType(".docx");
        string? caseZip = Validation.GetAttachedFileType(".zip");

        string? caseOtf = Validation.GetAttachedFileType(".otf");
        string? caseTtf = Validation.GetAttachedFileType(".ttf");
        string? caseWoff = Validation.GetAttachedFileType(".woff");

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
            Assert.That(caseMp4, Is.EqualTo("video/mp4"));
            Assert.That(caseWebm, Is.EqualTo("video/webm"));
            Assert.That(caseOgv, Is.EqualTo("video/ogg"));
            Assert.That(caseMpeg, Is.EqualTo("video/mpeg"));
            Assert.That(casePdf, Is.EqualTo("application/pdf"));
            Assert.That(caseJson, Is.EqualTo("application/json"));
            Assert.That(caseXml, Is.EqualTo("application/xml"));
            Assert.That(caseXls, Is.EqualTo("application/vnd.ms-excel"));
            Assert.That(caseXlsx, Is.EqualTo("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
            Assert.That(caseDoc, Is.EqualTo("application/msword"));
            Assert.That(caseDocx, Is.EqualTo("application/vnd.openxmlformats-officedocument.wordprocessingml.document"));
            Assert.That(caseZip, Is.EqualTo("application/zip"));
            Assert.That(caseOtf, Is.EqualTo("font/otf"));
            Assert.That(caseTtf, Is.EqualTo("font/ttf"));
            Assert.That(caseWoff, Is.EqualTo("font/woff"));
        });
    }

}