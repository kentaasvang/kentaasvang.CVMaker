using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LagDinCv.WebUI.Pages;

public class PreviewModel : PageModel
{
    public Uri? PreviewPath;
    
    public void OnGet(Uri? previewPath)
    {
        PreviewPath = previewPath;
    }
}