using kentaasvang.PdfLatex;
using kentaasvang.TemplatingEngine;

namespace LagDinCv.Api.Services;

public interface IPdfBuilderService
{
    Task<Uri> CreateResume(TwentySecondModel model);
}

public class PdfBuilderService : IPdfBuilderService
{
    public async Task<Uri> CreateResume(TwentySecondModel model)
    {
        var options = new PdfLatexSettings
        {
            OutputDir = "Resumes",
            TemplateDir = "Templates/",
            TempFilesDir = "Templates/TemporaryTemplates/"
        };

        var fileName = new TwentySecondCv().Name();

        var templateFile = Path.Combine(options.TemplateDir, fileName);
        var document = await File.ReadAllTextAsync(templateFile);

        var keyValues = model.ToDictionary();

        var template = TemplatingEngine.Replace(document, keyValues, true);
        var tempTemplateName = GetRandomFileNameWithExtension(FileExtension.Tex);

        await File.WriteAllTextAsync(
            Path.Combine(options.TempFilesDir, tempTemplateName), template
        );

        PdfLatexBuilder pdfLatexBuilder = new();
        GetRandomFileNameWithExtension(FileExtension.Pdf);

        pdfLatexBuilder
            .OutputDirectory(options.OutputDir)
            .File(Path.Combine(options.TempFilesDir, tempTemplateName))
            .EnableInstaller()
            .IncludeDirectory(options.TemplateDir)
            .NonStopMode();

        await pdfLatexBuilder.Run();

        var filePath = new Uri($"/{Path.GetFileNameWithoutExtension(tempTemplateName)}.pdf", UriKind.Relative);
        return filePath;
    }
    
    private string GetRandomFileNameWithExtension(FileExtension extension)
    {
        const string pdf = ".pdf";
        const string tex = ".tex";
        var fileName = Path.GetRandomFileName();

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