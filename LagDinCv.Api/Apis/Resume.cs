using LagDinCv.Api.Services;
using Microsoft.Net.Http.Headers;

namespace LagDinCv.Api.Apis;

internal static class Resume
{
    public static RouteGroupBuilder MapApi(this IEndpointRouteBuilder routeBuilder)
    {
        var group = routeBuilder.MapGroup("/api");
      
        group.MapGet("/hello", () => "world").RequireCors(opt =>
        {
            opt.AllowAnyOrigin();
        });

        group.MapPost("/create", async (TwentySecondModel model, IPdfBuilderService pdfBuilder) 
            =>
        {
            var filePath = await pdfBuilder.CreateResume(model);
            return Results.Created(filePath, new { });
        }).RequireCors(opt =>
        {
            // TODO: be thorough with these setting
            opt.AllowAnyOrigin();
            opt.AllowAnyHeader();
            opt.WithExposedHeaders(HeaderNames.Location);
        });

        group.MapGet("resume/{fileName}", (string fileName) 
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

        return group;
    }
}