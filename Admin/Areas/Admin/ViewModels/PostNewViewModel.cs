using System.ComponentModel.DataAnnotations;

namespace Admin.Areas.Admin.ViewModels
{
    public class PostNewViewModel
    {
        [Required]

        public string Title { get; set; }
        [Required]

        public string Body { get; set; }
        [Required]

        public IFormFile Image { get; set; }
    }
}
