using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace kentaasvang.CVMaker.Forms;

public class CvForm
{
    // TODO: this should not be hard coded
    public string TwentySecond = "twentysecond.template";
    
    // [Required(ErrorMessage = "Name is required", AllowEmptyStrings = false)]
    public string? Name { get; set; }
}