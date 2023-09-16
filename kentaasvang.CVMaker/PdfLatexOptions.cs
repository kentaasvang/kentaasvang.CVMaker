namespace kentaasvang.CVMaker;

public class PdfLatexOptions
{
    public const string PdfLatex = nameof(PdfLatex);
    public string OutputDir    { get; set; } = string.Empty;
    public string TemplateDir  { get; set; } = string.Empty;
    public string TempFilesDir { get; set; } = string.Empty;
}