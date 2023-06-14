using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Areas.Admin.ViewModels
{
    public class StudentViewModel
    {
        [Required]

        public string UniversityId { get; set; }
        [Required]

        public string Major { get; set; }
        [Required]

        public string Faculty { get; set; }
        [Required]

        public string Class { get; set; }
        [Required]

        public string Password { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Section Num")]
        public int Section_Num { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        public string? AdminId { get; set; }
        public string DeptId { get; set; }
        public string RankId { get; set; }
        public SelectList Departments { get; set; }
        public SelectList Ranks { get; set; }
    }
}
