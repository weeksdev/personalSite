using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace PersonalSite.Api.Pages
{
    public class ProjectViewModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? ProjectUrl { get; set; }
        public string? GitHubUrl { get; set; }
        public List<string> Technologies { get; set; } = new List<string>();
    }

    public class ProjectsModel : PageModel
    {
        public List<ProjectViewModel> Projects { get; set; } = new List<ProjectViewModel>();

        public void OnGet()
        {
            ViewData["Title"] = "My Projects";
            // Populate with placeholder/sample project data
            Projects = new List<ProjectViewModel>
            {
                new ProjectViewModel
                {
                    Title = "Colorado County App (ExtJS Demo)",
                    Description = "An ExtJS demo application showcasing data visualization for Colorado counties. This was featured on the original homepage.",
                    ImageUrl = "~/images/coloradoAppSS1.png", // Assumes image is in wwwroot/images
                    ProjectUrl = "http://andrewweeks.hopto.org/Web/WorkSpace/ExtJSCountyDemoApp/",
                    GitHubUrl = "https://github.com/weeksdev/ExtJSCountyDemoApp", // Example
                    Technologies = new List<string> { "ExtJS", "JavaScript", "HTML", "CSS", "Data Visualization" }
                },
                new ProjectViewModel
                {
                    Title = "Personal Blog & Portfolio (This Site)",
                    Description = "The website you are currently viewing. Built with ASP.NET Core Razor Pages, Entity Framework Core, and PostgreSQL. Features a dynamic blog and portfolio sections.",
                    ImageUrl = "https://via.placeholder.com/300x200.png?text=Portfolio+Site",
                    ProjectUrl = "/", // Link to home
                    GitHubUrl = "https://github.com/weeksdev/PersonalSite", // Actual repo if public
                    Technologies = new List<string> { "ASP.NET Core", "Razor Pages", "C#", "EF Core", "PostgreSQL", "Bootstrap" }
                },
                new ProjectViewModel
                {
                    Title = "E-commerce Platform API",
                    Description = "A RESTful API for a mock e-commerce platform, supporting product catalog, inventory, orders, and customer management. Built with ASP.NET Core Web API.",
                    ImageUrl = "https://via.placeholder.com/300x200.png?text=E-commerce+API",
                    ProjectUrl = "#", // No live site for a pure API usually
                    GitHubUrl = "https://github.com/weeksdev/EcommerceApiDemo", // Example
                    Technologies = new List<string> { "ASP.NET Core Web API", "C#", "REST", "JWT Auth", "SQL Server" }
                },
                new ProjectViewModel
                {
                    Title = "Data Analysis Dashboard",
                    Description = "A web-based dashboard for visualizing sales data, using Chart.js and a Python/Flask backend to process and serve data.",
                    ImageUrl = "https://via.placeholder.com/300x200.png?text=Data+Dashboard",
                    ProjectUrl = "#",
                    GitHubUrl = "https://github.com/weeksdev/DataDashboardDemo", // Example
                    Technologies = new List<string> { "Python", "Flask", "Pandas", "Chart.js", "Bootstrap" }
                }
            };
        }
    }
}
