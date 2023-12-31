using kentaasvang.CVMaker.Enums;
using kentaasvang.CVMaker.Models;

namespace kentaasvang.CVMaker.Requests;

public class CreateCvRequest
{
    public CvTemplateType CvTemplateType { get; set; }
    public string? Name { get; set; }
    public List<ExperienceModel>? Experiences { get; set; }
    public List<EducationModel>? Educations { get; set; }
    public string? JobTitle { get; set; }
    public DateTime? DayOfBirth { get; set; }
    public string? Nationality { get; set; }
    public string? PhoneNumber { get; set; }
    public string? EmailAddress { get; set; }
    public string? Website { get; set; }
    public string? Linkedin { get; set; }
    public string? About { get; set; }
    public List<Skill>? Skills { get; set; }

    // TODO: handle with RazorLight
    public Dictionary<string, string> ToDictionary()
    {
        var skills = string.Empty;
        if (Skills != null && Skills.Any())
        {
            skills = ParseSkills(Skills);
        }

        var experiences = string.Empty;
        if (Experiences != null && Experiences.Any())
        {
            experiences = ParseExperiences(Experiences);
        }

        var educations = string.Empty;
        if (Educations != null && Educations.Any())
        {
            educations = ParseEducations(Educations);
        }

        var dictionary = new Dictionary<string, string>
        {
            { "name", Name },
            { "jobtitle", JobTitle ?? string.Empty },
            { "experiences", experiences },
            { "educations", educations },
            { "dayofbirth", DayOfBirth?.ToString("M-d-yyyy") ?? string.Empty },
            { "nationality", Nationality ?? string.Empty },
            { "phonenumber", PhoneNumber ?? string.Empty },
            { "website", Website ?? string.Empty },
            { "emailaddress", EmailAddress },
            { "linkedin", Linkedin ?? string.Empty },
            { "about", About ?? string.Empty },
            { "skill", skills }
        };

        return dictionary;
    }

    private static string ParseSkills(IEnumerable<Skill> skills)
    {
        var parsed = "";
        foreach (var skill in skills)
            parsed = parsed + "{" + skill.Name + "/" + skill.Rating + "},";

        // remove last comma
        parsed = parsed[..^1];
        return parsed;
    }

    private static string ParseExperiences(IEnumerable<ExperienceModel> experiences)
    {
        var start = @"
\section{Erfaring}

\begin{twenty} % Environment for a list with descriptions
";

        var end = @"
\end{twenty}
";
        var xp = "";
        foreach (var experience in experiences)
        {
            xp += @"\twentyitem{" + experience.From.ToString("yyyy") + "-" + experience.To.ToString("yyyy") + "}{" +
                  experience.Company + "}{" + experience.Title +
                  "}{" + experience.Description + "}\n";
        }

        return start + xp + end;
    }

    // TODO: handle this in template with RazorLight
    private static string ParseEducations(IEnumerable<EducationModel> educations)
    {
        var start = @"
\section{Utdanning}

\begin{twenty} % Environment for a list with descriptions
";

        var end = @"
\end{twenty}
";
        var xp = "";
        foreach (var education in educations)
        {
            xp += @"\twentyitem{" + education.From.ToString("yyyy") + "-" + education.To.ToString("yyyy") + "}{" +
                  education.SchoolName + "}{" + education.Location +
                  "}{" + education.Description + "}\n";
        }

        return start + xp + end;
    }
}

public class Skill
{
    public string? Name { get; set; }
    public double Rating { get; set; }
}