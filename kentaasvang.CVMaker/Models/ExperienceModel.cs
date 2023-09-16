namespace kentaasvang.CVMaker.Models;

public class ExperienceModel
{
    public required string Title { get; set; }
    public required string Company { get; set; }
    public string? Description { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}