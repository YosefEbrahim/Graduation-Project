using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Models;

namespace Admin.Areas.Admin.ViewModels
{
    public class RanksViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    

    }
    public class PostRanksViewModel
    {
        public string Name { get; set; }
        public string DeptId { get; set; }
        public SelectList Departments { get; set; }

    }

}
