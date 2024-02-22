using iText.Html2pdf;
using iText.IO.Source;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using System.Text;
namespace IText7.HtmlToPdf.Service;
public class PdfProcessService : IPdfProcessService
{      

    /// <summary>
         /// GELEN HTML CONTENTİ BYTE ARRAYA DÖNÜŞTÜRÜYOR
         /// </summary>
         /// <param name="htmlContent"></param>
         /// <returns></returns>
    public  byte[] PdfConvert(string htmlContent)
    {
        using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(htmlContent)))
        {
            ConverterProperties converterProperties = new ConverterProperties();
            ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
            PdfWriter writer = new PdfWriter(byteArrayOutputStream);
            PdfDocument pdfDocument = new PdfDocument(writer);
            Document doc = new Document(pdfDocument);
            pdfDocument.SetDefaultPageSize(PageSize.A4);
            HtmlConverter.ConvertToPdf(stream, pdfDocument, converterProperties);
            pdfDocument.Close();
            return byteArrayOutputStream.ToArray();
        }
    }


    /// <summary>
    /// GELEN HTML Dictionary LİSTESİNİ BİRLEŞTİRİP YENİ BYTE ARRAY VERİYOR.
    /// </summary>
    /// <param name="htmlContent"></param>
    /// <returns></returns>
    public  byte[] ByteArrayCombine(byte[] firstPdf,Dictionary<int, byte[]> pdfByteArrayList)
    {
        using (MemoryStream ms = new MemoryStream())
        using (PdfDocument pdf = new PdfDocument(new PdfWriter(ms).SetSmartMode(true)))
        {
            using (MemoryStream memoryStream = new MemoryStream(firstPdf))
            {
                using (PdfReader reader = new PdfReader(memoryStream))
                {
                    PdfDocument srcDoc = new PdfDocument(reader);
                    srcDoc.CopyPagesTo(1, srcDoc.GetNumberOfPages(), pdf);
                }
            }

            foreach (var item in pdfByteArrayList.OrderBy(t => t.Key))
            {
                using (MemoryStream memoryStream = new MemoryStream(item.Value))
                {
                    using (PdfReader reader = new PdfReader(memoryStream))
                    {
                        PdfDocument srcDoc = new PdfDocument(reader);
                        srcDoc.CopyPagesTo(1, srcDoc.GetNumberOfPages(), pdf);
                    }
                }
            }

            pdf.Close();
            return ms.ToArray();
        }
    }


    public  string AsciiCode(string src)
    {
        src = src
            .Replace("Ç", "&#199;")
            .Replace("ç", "&#231;")
            .Replace("Ğ", "&#286;")
            .Replace("ğ", "&#287;")
            .Replace("Ü", "&#220;")
            .Replace("ü", "&#252;")
            .Replace("İ", "&#304;")
            .Replace("I", "&#73;")
            .Replace("ı", "&#305;")
            .Replace("Ö", "&#214;")
            .Replace("ö", "&#246;")
            .Replace("Ş", "&#350;")
            .Replace("ş", "&#351;");
        return src;
    }


    public  string PdfName()
    {
        Random random = new Random();
        var name = random.Next(1, 454545);
        var pdfName = name + "-" + "test.pdf";
        return pdfName;
    }
}