using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Models;

namespace Admin.Areas.Admin.ViewModels
{
    public class TeamsViewModel
    {
        public IFormFile Logo { get; set; }
        public string? LogoPath { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }

}
