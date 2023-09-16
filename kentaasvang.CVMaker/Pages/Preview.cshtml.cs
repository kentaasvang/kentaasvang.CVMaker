using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace kentaasvang.CVMaker.Pages;

public class PreviewModel : PageModel
{
    public string? PreviewPath;

    public void OnGet([FromQuery] Uri? previewPath)
    {
        if (previewPath != null)
        {
            PreviewPath = Path.Combine("Resumes", previewPath.ToString()[1..]);
        }
    }
}