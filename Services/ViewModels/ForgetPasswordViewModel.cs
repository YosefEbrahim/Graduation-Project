using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels
{
    public class ForgetPasswordViewModel
    {
        public class Request
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }
        public class Response
        {
            public string Message { get; set; }

        }
    }
}
