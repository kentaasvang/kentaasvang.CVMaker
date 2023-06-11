using System.Net.NetworkInformation;
using LagDinCv.Domain.Enums;
using LagDinCv.Domain.Requests;
using LagDinCv.Application.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LagDinCv.WebUI.Pages;

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
    
    // public async Task<IActionResult> OnPost(CreateCvRequest createCvRequest)
    public async Task OnPost(CreateCvRequest createCvRequest)
    {
        createCvRequest.CvTemplateType = CvTemplateType.TwentySecond;
        var filePath = await _pdfBuilder.CreateResume(createCvRequest);
         
        // return new CreatedResult(filePath, new { });
    }
}