using System.Net.NetworkInformation;
using LagDinvCv.Application.Services;
using LagDinCv.Domain.Enums;
using LagDinCv.Domain.Interfaces;
using LagDinCv.Domain.Requests;
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
    
    public async Task<IActionResult> OnPost(CreateCvRequest createCvRequest)
    {
        createCvRequest.CvTemplateType = CvTemplateType.TwentySecond;
        var filePath = await _pdfBuilder.CreateResume(createCvRequest);
        return new CreatedResult(filePath, new { });
    }
}