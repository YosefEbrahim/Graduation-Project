using System.ComponentModel.DataAnnotations;

namespace Services.Areas.External.ViewModels
{
    public class VerifyResetPasswordViewModel
    {
        [Required]
        public string Token { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
