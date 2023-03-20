namespace LagDinCv.Api.Models;

public class ExperienceModel
{
    public string? Description { get; set; }
    public required string Title { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    
}