using System.ComponentModel.DataAnnotations;

namespace Birder2.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
