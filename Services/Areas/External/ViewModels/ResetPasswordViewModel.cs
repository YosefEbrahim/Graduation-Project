using System.ComponentModel.DataAnnotations;

namespace Services.Areas.External.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Token { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string NewPassword { get; set; }
        [Compare("NewPassword")]
        [Required]
        public string ConfirmNewPassword { get; set; }
    }
}
