using System.ComponentModel.DataAnnotations;

namespace BLL.Models;

public class AdminCreateModel
{
    [Required(ErrorMessage = "Please provide a username.")]
    [StringLength(50, ErrorMessage = "Username cannot be more than 50 characters.")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please provide an email address.")]
    [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
    [StringLength(50, ErrorMessage = "Email cannot be more than 50 characters.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please provide a password.")]
    [StringLength(50, MinimumLength = 4, ErrorMessage = "Password must be between 4 and 50 characters.")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please confirm the password.")]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
