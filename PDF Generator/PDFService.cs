using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using TravelPlanner.Domain.Interfaces.PDF;
using TravelPlanner.Domain.Models.Entities;

namespace PDF_Generator;

public class PDFService : IPDFService
{
    IRazorViewToStringRenderer stringrender;

    public PDFService(IRazorViewToStringRenderer stringrender)
    {
        this.stringrender = stringrender;
    }

    public async Task<FileContentResult> GenerateQuotation(Quotation quotation)
    {
        var html = await stringrender.RenderViewToStringAsync("/Views/Pdf/Quotation.cshtml", quotation);
        var pdfBytes = await GeneratePdfFromHtmlAsync(html);

        return new FileContentResult(pdfBytes, "application/pdf")
        {
            FileDownloadName = "quotation.pdf"
        };
    }

    private async Task<byte[]> GeneratePdfFromHtmlAsync(string html)
    {
        await new BrowserFetcher().DownloadAsync(); // Ensures Chromium is downloaded

        await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
        await using var page = await browser.NewPageAsync();

        await page.SetContentAsync(html);

        return await page.PdfDataAsync(new PdfOptions
        {
            Format = PaperFormat.A4,
            PrintBackground = true
        });
    }
}
