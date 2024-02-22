namespace IText7.HtmlToPdf.Model;
public class PdfResponse
{
    public string Base64 { get; set; }
    public byte[] PdfArray { get; set; }
    public string PdfName { get; set; }
}