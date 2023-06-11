using LagDinCv.Domain.Enums;
using LagDinCv.Domain.Requests;
using LagDinCv.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LagDinCv.WebUI.Pages;

[Authorize]
public class CreateModel : PageModel
{
    private readonly ILogger<CreateModel> _logger;
    private readonly IPdfBuilder _pdfBuilder;
    public Uri? CvFilePath;

    public CreateModel(ILogger<CreateModel> logger, IPdfBuilder pdfBuilder)
    {
        _logger = logger;
        _pdfBuilder = pdfBuilder;
    }
    
    public void OnGet()
    {
        
    }
    
    // TODO: add CV to Db on correct User
    public async Task OnPost(CreateCvRequest createCvRequest)
    {
        createCvRequest.CvTemplateType = CvTemplateType.TwentySecond;
        // TODO: Add error-handling
        var filePath = await _pdfBuilder.CreateResume(createCvRequest);
        CvFilePath = filePath;
    }
}