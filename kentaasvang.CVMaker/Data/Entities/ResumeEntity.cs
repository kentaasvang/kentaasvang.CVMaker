namespace kentaasvang.CVMaker.Data.Entities;

public class ResumeEntity
{
    public Guid Id { get; set; }    
    public string? Name { get; set; }
    public UserEntity? Owner { get; set; }
}