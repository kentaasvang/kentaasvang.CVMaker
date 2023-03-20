namespace LagDinCv.Api.Models;

public class EducationModel
{
    public required string SchoolName { get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}