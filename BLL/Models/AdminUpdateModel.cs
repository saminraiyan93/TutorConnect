using System.ComponentModel.DataAnnotations;

namespace BLL.Models;

public class AdminUpdateModel
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "Please provide a username.")]
    [StringLength(50, ErrorMessage = "Username cannot be more than 50 characters.")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please provide an email address.")]
    [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
    [StringLength(50, ErrorMessage = "Email cannot be more than 50 characters.")]
    public string Email { get; set; } = string.Empty;
}
