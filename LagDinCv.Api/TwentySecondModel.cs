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
    public List<Skill>? Skills { get; set; }


    public Dictionary<string, string> ToDictionary()
    {
        var skills = string.Empty;
        if (Skills != null && Skills.Any())
        {
            skills = "";
            
            foreach (var skill in Skills)
            {
                skills += "{" + skill.Name + "/" + skill.Rating + "},";
            }
            
            // remove last comma
            skills = skills[..^1];
        }
        
        var dictionary = new Dictionary<string, string>
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
            { "skill", skills }
        };

        return dictionary;
    }
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