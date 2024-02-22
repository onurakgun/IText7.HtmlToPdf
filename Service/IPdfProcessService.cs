namespace IText7.HtmlToPdf.Service;
public interface IPdfProcessService
{
    byte[] PdfConvert(string htmlContent);

    byte[] ByteArrayCombine(byte[] firtPdf,Dictionary<int, byte[]> pdfByteArrayList);

    string AsciiCode(string src);

    string PdfName();
}
