using System.ComponentModel.DataAnnotations;

namespace kentaasvang.CVMaker.Forms;

public class CvForm
{
    // TODO: this should not be hard coded
    public string TwentySecond = "twentysecond.template";

    [DataType(DataType.Text)]
    public string? Name { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime? DateOfBirth { get; set; }
    
    public string? JobTitle { get; set; }
    
    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber { get; set; }
    
    [DataType(DataType.Text)]
    public string? Address { get; set; }
}