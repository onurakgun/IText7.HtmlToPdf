using IText7.HtmlToPdf.Model;
using IText7.HtmlToPdf.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
namespace IText7.HtmlToPdf.Controllers;
public class HomeController : Controller
{
    #region DEPENDENCY INJECTION
    private readonly IPdfProcessService _processService;
    public HomeController(IPdfProcessService processService)
    {
        _processService = processService;
    }
    #endregion

    public IActionResult Index()
    {
        string textFile = @"C:\Users\Onur\Itext7.HtmlToPdf-master\Itext7.HtmlToPdf\Views\Home\pdf.html";
        string Content = System.IO.File.ReadAllText(textFile);
        byte[] pdfArray = _processService.PdfConvert(_processService.AsciiCode(Content));
        var base64 = Convert.ToBase64String(pdfArray);
        PdfResponse pdfResponse = new PdfResponse
        {
            Base64 = base64,
            PdfArray = pdfArray,
            PdfName = _processService.PdfName()
        };
        return File(pdfResponse.PdfArray, "application/pdf", pdfResponse.PdfName);
    }

    public IActionResult PdfMerge()
    {
        string textFile = @"C:\Users\Onur\Itext7.HtmlToPdf-master\Itext7.HtmlToPdf\Views\Home\pdf.html";
        string Content = System.IO.File.ReadAllText(textFile);
        byte[] pdfArray = _processService.PdfConvert(_processService.AsciiCode(Content));


        var ÝmageFile = @"C:\Users\Onur\Itext7.HtmlToPdf-master\Itext7.HtmlToPdf\Views\Home\image.html";
        var ContentImage = System.IO.File.ReadAllText(ÝmageFile);
        byte[] pdfArray2 = _processService.PdfConvert(_processService.AsciiCode(ContentImage));

        Dictionary<int, byte[]> dictionary = new Dictionary<int, byte[]>
        {
            { 1, pdfArray2 }
        };
        var merge = _processService.ByteArrayCombine(pdfArray, dictionary);
        var base64 = Convert.ToBase64String(merge);
        PdfResponse pdfResponse = new PdfResponse
        {
            Base64 = base64,
            PdfArray = merge,
            PdfName = _processService.PdfName()
        };
        return File(pdfResponse.PdfArray, "application/pdf", pdfResponse.PdfName);
    }
}