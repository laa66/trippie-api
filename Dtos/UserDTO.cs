using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class UserDTO(string email, string password, string confirmPassword, string username)
{
    [Required]
    public string Email { get; set; } = email;

    [Required]
    public string Password { get; set; } = password;

    [Required]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = confirmPassword;

    [Required]
    public string Username { get; set; } = username;


}