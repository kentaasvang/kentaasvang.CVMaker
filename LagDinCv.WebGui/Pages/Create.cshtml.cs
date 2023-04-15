using System.Net.NetworkInformation;
using LagDinCv.Api.Services;
using LagDinCv.Domain.Enums;
using LagDinCv.Domain.Interfaces;
using LagDinCv.Domain.Requests;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LagDinCv.WebGui.Pages;

public class CreateModel : PageModel
{
    private readonly ILogger<CreateModel> _logger;
    private readonly IPdfBuilder _pdfBuilder;

    public CreateModel(ILogger<CreateModel> logger, IPdfBuilder pdfBuilder)
    {
        _logger = logger;
        _pdfBuilder = pdfBuilder;
    }
    
    public void OnGet()
    {
        
    }
    
    public async Task<IActionResult> OnPost(CreateCvRequest createCvRequest)
    {
        createCvRequest.CvTemplateType = CvTemplateType.TwentySecond;
        var filePath = await _pdfBuilder.CreateResume(createCvRequest);
        // return Results.Created(filePath, new { });
        return new CreatedResult(filePath, new { });

        // group.MapPost("/create", async (TwentySecondModel model, IPdfBuilderService pdfBuilder) 
        //     =>
        // {
        //     var filePath = await pdfBuilder.CreateResume(model);
        //     return Results.Created(filePath, new { });
        // }).RequireCors(opt =>
        // {
        //     // TODO: be thorough with these setting
        //     opt.AllowAnyOrigin();
        //     opt.AllowAnyHeader();
        //     opt.WithExposedHeaders(HeaderNames.Location);
        // });
    }
}