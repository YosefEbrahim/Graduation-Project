using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Models;
using System.ComponentModel.DataAnnotations;

namespace Admin.Areas.Admin.ViewModels
{
    public class TableViewModel
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string DeptId { get; set; }
        [Required]
        public string RankId { get; set; }
        [Required]
        public int Section_Num { get; set; }
    }


}
