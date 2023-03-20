namespace LagDinCv.Api.Models;

public class Experience
{
    // \\twentyitem{2020-}{Lynx Publishing}{Fullstack Developer and DevOps}{Sole developer and DevOps engineer at Lynx Publishing, a content management system for newspapers. }
    public string? Description { get; set; }
    public required string Title { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    
}