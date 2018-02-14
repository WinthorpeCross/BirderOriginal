using System.ComponentModel.DataAnnotations;

namespace Birder2.ViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
