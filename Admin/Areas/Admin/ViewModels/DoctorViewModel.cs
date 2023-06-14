using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Admin.Areas.Admin.ViewModels
{
    public class DoctorViewModel
    {
        [Required]
        public string Specialice { get; set; }
        [Required]
        [Display(Name= "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        public SelectList Departments { get; set; }
        public string DeptId { get; set; }
        public SelectList Subjects { get; set; }
        public string [] SubjectsId { get; set; }

    }

    public class DoctorEditViewModel
    {
        [Required]
        public string Specialice { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        public SelectList Departments { get; set; }
        public string DeptId { get; set; }

    }
}
