using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace PersonalSite.Api.Pages
{
    public class AboutModel : PageModel
    {
        public string? PageTitle { get; set; }
        public string? Introduction { get; set; }
        public List<SkillCategory> SkillCategories { get; set; } = new List<SkillCategory>();
        public List<ExperienceItem> ExperienceEntries { get; set; } = new List<ExperienceItem>();
        public List<EducationItem> EducationEntries { get; set; } = new List<EducationItem>();

        public void OnGet()
        {
            ViewData["Title"] = "About Me";
            PageTitle = "About Andrew Weeks";
            Introduction = "Hello! I'm Andrew, a software developer with a passion for creating efficient and scalable web applications, working with data, and developing robust middleware solutions. With years of experience in the field, I've had the opportunity to work with a diverse range of technologies and tackle complex challenges in various domains, including finance and data analytics.";

            SkillCategories = new List<SkillCategory>
            {
                new SkillCategory { Name = "Backend Development", Skills = new List<string> { "C#", ".NET (Core, Framework)", "ASP.NET (Core MVC, Web API, Razor Pages)", "Entity Framework (Core)", "Node.js (Basic)" } },
                new SkillCategory { Name = "Frontend Development", Skills = new List<string> { "HTML5", "CSS3", "JavaScript", "jQuery", "Bootstrap", "ExtJS (Legacy)", "WPF/Silverlight (Legacy)" } },
                new SkillCategory { Name = "Databases & Data", Skills = new List<string> { "MS SQL Server", "PostgreSQL", "Netezza", "Intersystems Cache", "SQL", "Data Modeling", "ETL Processes" } },
                new SkillCategory { Name = "Tools & Methodologies", Skills = new List<string> { "Git", "Docker (Basic)", "Agile/Scrum", "CI/CD (Basic Concepts)" } },
                new SkillCategory { Name = "Financial Domain Knowledge", Skills = new List<string> { "Securities Events", "Alternative Investments", "ACATS", "Risk Data" } }
            };

            ExperienceEntries = new List<ExperienceItem>
            {
                new ExperienceItem
                {
                    JobTitle = "Senior Software Developer",
                    Company = "Tech Solutions Inc.",
                    Dates = "Jan 2020 - Present",
                    Responsibilities = new List<string> { "Led development of key features for a flagship product.", "Mentored junior developers.", "Improved application performance by 20%." }
                },
                new ExperienceItem
                {
                    JobTitle = "Software Developer",
                    Company = "Innovatech Ltd.",
                    Dates = "Jun 2017 - Dec 2019",
                    Responsibilities = new List<string> { "Developed and maintained web applications using ASP.NET MVC and Angular.", "Collaborated with cross-functional teams.", "Contributed to database design and optimization." }
                }
            };

            EducationEntries = new List<EducationItem>
            {
                new EducationItem
                {
                    Degree = "Master of Science in Computer Science",
                    Institution = "University of Technology",
                    Year = "2017",
                    Notes = "Specialized in Software Engineering."
                },
                new EducationItem
                {
                    Degree = "Bachelor of Science in Information Systems",
                    Institution = "State College",
                    Year = "2015",
                    Notes = "Graduated with honors."
                }
            };
        }
    }

    public class SkillCategory
    {
        public string? Name { get; set; }
        public List<string> Skills { get; set; } = new List<string>();
    }

    public class ExperienceItem
    {
        public string? JobTitle { get; set; }
        public string? Company { get; set; }
        public string? Dates { get; set; }
        public List<string> Responsibilities { get; set; } = new List<string>();
    }

    public class EducationItem
    {
        public string? Degree { get; set; }
        public string? Institution { get; set; }
        public string? Year { get; set; }
        public string? Notes { get; set; }
    }
}
