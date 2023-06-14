using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels
{
    public class ChangePasswordViewModel
    {
        public class Request
        {
            [Required]
            [Display(Name ="Current Password")]
            public string CurrentPassword { get; set; }
            [Required]
            [Display(Name = "New Password")]

            public string NewPassword { get; set; }
            [Compare("NewPassword")]
            [Required]
            [Display(Name = "Confirm New Password")]

            public string ConfirmNewPassword { get; set; }

        }
        public class Response
        {
            public string Message { get; set; }

        }
    }
}
