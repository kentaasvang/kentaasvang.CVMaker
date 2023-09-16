using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LagDinCv.WebUI.Pages;

public class PreviewModel : PageModel
{
    public string? PreviewPath;
    
    public void OnGet(Uri? previewPath)
    {
        if (previewPath is null)
            return;
        
        PreviewPath = Path.Combine("Resumes", previewPath.ToString()[1..]);
    }
}