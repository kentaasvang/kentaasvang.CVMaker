using LagDinCv.Api.Models;

namespace LagDinCv.Api;

public class TwentySecondModel : ITemplateModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<ExperienceModel>? Experiences { get; set; }
    public List<EducationModel>? Educations { get; set; }
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
            skills = ParseSkills(Skills);
        }
        
        var dictionary = new Dictionary<string, string>
        {
            { "name", Name ?? string.Empty },
            { "jobtitle", JobTitle ?? string.Empty },
            { "dayofbirth", DayOfBirth?.ToString("M-d-yyyy") ?? string.Empty },
            { "nationality", Nationality ?? string.Empty },
            { "phonenumber", PhoneNumber ?? string.Empty },
            { "website", Website ?? string.Empty },
            { "emailaddress", EmailAddress ?? string.Empty },
            { "linkedin", Linkedin ?? string.Empty },
            { "about", About ?? string.Empty },
            { "skill", skills }
        };

        // \\twentyitem{2020-}{Lynx Publishing}{Fullstack Developer and DevOps}{Sole developer and DevOps engineer at Lynx Publishing, a content management system for newspapers. }
        // \\twentyitem {2016-2019}{Noroff's University}{Kristiansand/Nettbasert}{Bachelor of Applied Data Science}
        return dictionary;
    }

    private static string ParseSkills(IEnumerable<Skill> skills)
    {
        var parsed = skills.Aggregate("", (current, skill) => current + "{" + skill.Name + "/" + skill.Rating + "},");

        // remove last comma
        parsed = parsed[..^1];
        return parsed;
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