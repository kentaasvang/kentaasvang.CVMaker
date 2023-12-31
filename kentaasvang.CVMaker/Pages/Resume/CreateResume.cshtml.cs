using kentaasvang.CVMaker.Forms;
using kentaasvang.CVMaker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace kentaasvang.CVMaker.Pages.Resume;

// [Authorize]
public class CreateResumePageModel : PageModel
{
    private readonly PdfManager _pdfManager;

    [BindProperty] 
    public CvForm? Form { get; set; }

    // private readonly ApplicationDbContext _dbContext;
    // private readonly UserManager<UserEntity> _userManager;
    public string? Path;

    // public CreateResumePageModel(PdfBuilder pdfBuilder, ApplicationDbContext dbContext,
    //     UserManager<UserEntity> userManager)
    public CreateResumePageModel(PdfManager pdfManager)
    {
        _pdfManager = pdfManager;
        // _dbContext = dbContext;
        // _userManager = userManager;
    }

    public PageResult OnGet()
    {
        return Page();
    }

    public async Task<PageResult> OnPost()
    {
        if (ModelState.IsValid && Form != null)
        {
            Path = await _pdfManager.CreateAsync(Form);
        }

        return Page();

        // var identityIsAuthenticated = User.Identity != null && User.Identity.IsAuthenticated;
        // var identityIsAuthenticated = true;
        // if (identityIsAuthenticated)
        // {
        //     // var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
        //     //              ?? throw new NullReferenceException();
        //
        //     // var user = await _userManager.FindByIdAsync(userId);
        //
        //     createCvRequest.CvTemplateType = CvTemplateType.TwentySecond;
        //     var filePath = await _pdfBuilder.CreateResume(createCvRequest);
        //
        //     // remove ".pdf" from Uri
        //     // filePath = new Uri(filePath.ToString().Replace(".pdf", ""));
        //
        //     // var resumeEntity = new ResumeEntity
        //     // {
        //     //     Id = new Guid(filePath.ToString()),
        //     //     // Owner = user
        //     // };
        //
        //     CvFilePath = filePath.ToString().Substring(1);
        // }
        // else
        // {
        //     throw new Exception("fix shit");
        // }
    }
}