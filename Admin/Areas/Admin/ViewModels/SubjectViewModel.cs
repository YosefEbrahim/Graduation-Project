using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Models;

namespace Admin.Areas.Admin.ViewModels
{
    public class SubjectViewModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public SelectList Departments { get; set; }
    }
    public class PostSubjectViewModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string DeptId { get; set; }
    }
}
