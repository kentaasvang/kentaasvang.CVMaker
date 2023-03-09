namespace LagDinCv.Api;

public class TwentySecondModel : ITemplateModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? JobTitle { get; set; }
    public DateTime? DayOfBirth { get; set; }
    public string? Nationality { get; set; }
    public string? PhoneNumber { get; set; }
    public required string EmailAddress { get; set; }
    public string? Website { get; set; }
    public string? Linkedin { get; set; }
    public string? About { get; set; }
    public Skill? Skill { get; set; }


    public Dictionary<string, string> ToDictionary()
        => new()
        {
            { "name", Name ?? string.Empty },
            { "jobtitle", JobTitle ?? string.Empty },
            { "dayofbirth", DayOfBirth?.ToString("D") ?? string.Empty },
            { "nationality", Nationality ?? string.Empty },
            { "phonenumber", PhoneNumber ?? string.Empty },
            { "website", Website ?? string.Empty },
            { "emailaddress", EmailAddress ?? string.Empty },
            { "linkedin", Linkedin ?? string.Empty },
            { "about", About ?? string.Empty },
            { "skill", Skill?.Name != null ? "{" + Skill.Name + "/" + Skill.Rating + "}" : string.Empty }
        };
}

public class Skill
{
    public string? Name { get; set; }
    public double Rating { get; set; }
}

public interface ITemplateModel
{
    public int Id { get; set; }
    public Dictionary<string, string> ToDictionary();
}