using System.ComponentModel.DataAnnotations;

namespace Birder2.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        //[EmailAddress]
        [Display(Name = "E-mail or Username")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
