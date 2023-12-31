using kentaasvang.CVMaker.Forms;
using kentaasvang.CVMaker.Requests;
using kentaasvang.PdfLatex;
using RazorLight;

namespace kentaasvang.CVMaker.Services;

public class PdfManager
{
    private readonly FileWriter _fileWriter;
    private readonly PdfLatexOptions _options;

    public PdfManager(FileWriter fileWriter, IConfiguration configuration)
    {
        _fileWriter = fileWriter;
        _options = configuration.GetSection(PdfLatexOptions.PdfLatex).Get<PdfLatexOptions>()
                   ?? throw new NullReferenceException("PdfLatexOptions can't be null");
    }

    public async Task<string> CreateAsync(CvForm form)
    {
        var engine = new RazorLightEngineBuilder()
            .UseEmbeddedResourcesProject(typeof(CvForm))
            .SetOperatingAssembly(typeof(CvForm).Assembly)
            .Build();
        
        var templatePath = Path.Combine(_options.TemplateDir, form.TwentySecond);
        var template = await File.ReadAllTextAsync(templatePath);
        var tempTemplateName = GetRandomFileNameWithExtension(FileExtension.Tex);
        var dirPath = Path.Combine(_options.TempFilesDir, tempTemplateName);
        var result = await engine.CompileRenderStringAsync("key", template, form);
        var pdfLatexBuilder = new PdfLatexBuilder();
        var filePath = await _fileWriter.WriteContentToFileAsync(result, dirPath);
        
        pdfLatexBuilder
            .OutputDirectory(_options.OutputDir)
            .File(filePath)
            .EnableInstaller()
            .IncludeDirectory(_options.TemplateDir)
            .NonStopMode();
        
        await pdfLatexBuilder.Run();
        
        var urlPath = new Uri($"/{Path.GetFileNameWithoutExtension(tempTemplateName)}.pdf", UriKind.Relative);
        
        return urlPath.ToString().Substring(1);
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