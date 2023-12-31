using kentaasvang.CVMaker;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddWebUiComponents();
builder.Services.AddRazorPages();
builder.Services.AddPdfBuilder();
builder.Services.AddFileWriter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// adds Resume-folder to path so it can be displayed frontend 
var pdfLatexOptions =
    app.Services.GetService<IConfiguration>()?.GetSection(PdfLatexOptions.PdfLatex).Get<PdfLatexOptions>()
    ?? throw new NullReferenceException("Can't be null");

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(pdfLatexOptions.OutputDir),
    RequestPath = "/Resumes"
});

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();