using Admin.Areas.Admin.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels
{
    public class CheckLoginApiModel
    {

        public class Request
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            public string Password { get; set; }
            public bool Remmember { get; set; }

        }
        public class Response
        {
            public string Id { get; set; }
            public string Token { get; set; }
            public int Type { get; set; }
            public IEnumerable<NewsDetails> News { get; set; }

        }
    }
}
