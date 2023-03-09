using kentaasvang.PdfLatex;
using kentaasvang.TemplatingEngine;
using LagDinCv;
using LagDinCv.Api;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "hello, world");

app.MapPost("/api/create", async (TwentySecondModel model) =>
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

    return StatusCodes.Status201Created;
});

app.Run();

string GetRandomFileNameWithExtension(FileExtension extension)
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