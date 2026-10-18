using System.ComponentModel.DataAnnotations;

namespace AppLayerMVC.Models.Account
{
    public class EditProfileNameModel
    {
        [Required(ErrorMessage = "Please provide username")]
        [StringLength(50, ErrorMessage = "Username cannot be more than 50 characters")]
        public string UserName { get; set; } = null!;
    }

    public class EditProfileEmailModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }

    public class EditProfilePasswordModel
    {
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Password { get; set; } = null!;

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
