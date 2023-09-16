using kentaasvang.CVMaker.Requests;
using kentaasvang.PdfLatex;

namespace kentaasvang.CVMaker.Services;

public class PdfBuilder
{
    private readonly FileWriter _fileWriter;
    private readonly PdfLatexOptions _options;

    public PdfBuilder(FileWriter fileWriter, IConfiguration configuration)
    {
        _fileWriter = fileWriter;
        _options = configuration.GetSection(PdfLatexOptions.PdfLatex).Get<PdfLatexOptions>()
                   ?? throw new NullReferenceException("PdfLatexOptions can't be null");
    }

    public async Task<Uri> CreateResume(CreateCvRequest model)
    {
        var templateFileName = model.CvTemplateType.ToFileName();
        var templateFilePath = Path.Combine(_options.TemplateDir, templateFileName);
        var document = await File.ReadAllTextAsync(templateFilePath);

        var keyValues = model.ToDictionary();
        var template = TemplatingEngine.TemplatingEngine.Replace(document, keyValues, true);
        var tempTemplateName = GetRandomFileNameWithExtension(FileExtension.Tex);

        var dirPath = Path.Combine(_options.TempFilesDir, tempTemplateName);
        var filePath = await _fileWriter.WriteContentToFileAsync(template, dirPath);

        PdfLatexBuilder pdfLatexBuilder = new();
        GetRandomFileNameWithExtension(FileExtension.Pdf);

        pdfLatexBuilder
            .OutputDirectory(_options.OutputDir)
            .File(filePath)
            .EnableInstaller()
            .IncludeDirectory(_options.TemplateDir)
            .NonStopMode();

        await pdfLatexBuilder.Run();

        var urlPath = new Uri($"/{Path.GetFileNameWithoutExtension(tempTemplateName)}.pdf", UriKind.Relative);
        return urlPath;
    }

    private string GetRandomFileNameWithExtension(FileExtension extension)
    {
        const string pdf = ".pdf";
        const string tex = ".tex";
        var fileName = Guid.NewGuid().ToString();

        fileName = Path.GetFileNameWithoutExtension(fileName);

        var ext = extension switch
        {
            FileExtension.Pdf => pdf,
            FileExtension.Tex => tex,
            _ => throw new NotSupportedException()
        };

        fileName = Path.ChangeExtension(fileName, ext);

        return fileName;
    }

    enum FileExtension
    {
        Pdf,
        Tex
    }
}