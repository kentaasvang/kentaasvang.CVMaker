using kentaasvang.PdfLatex;
using kentaasvang.TemplatingEngine;
using LagDinCv;
using LagDinCv.Api;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.MapGet("/hello", () => "world").RequireCors(opt =>
{
    opt.AllowAnyOrigin();
});

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

    var filePath = new Uri($"/{Path.GetFileNameWithoutExtension(tempTemplateName)}.pdf", UriKind.Relative);

    return Results.Created(filePath, new { });
}).RequireCors(opt =>
{
    // TODO: be thorough with these setting
    opt.AllowAnyOrigin();
    opt.AllowAnyHeader();
    opt.WithExposedHeaders(HeaderNames.Location);
});

app.MapGet("resume/{fileName}", (string fileName) 
    =>
{
    var options = new PdfLatexSettings
    {
        OutputDir = "Resumes",
        TemplateDir = "Templates/",
        TempFilesDir = "Templates/TemporaryTemplates/"
    };
    var rootDir = Environment.CurrentDirectory;
    var resumesDir = Path.Combine(rootDir, options.OutputDir);
    
    return Results.File(Path.Combine(resumesDir, fileName), contentType: "application/pdf");
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