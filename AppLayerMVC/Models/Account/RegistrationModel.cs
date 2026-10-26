using System.ComponentModel.DataAnnotations;

namespace AppLayerMVC.Models.Account
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Please provide username")]
        [StringLength(50, ErrorMessage = "Username cannot be more than 50 characters")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        public string ? Role { get; set; }

        public string ? AccountStatus { get; set; }
    }
}