using System.ComponentModel.DataAnnotations;

namespace WebApp.Data.Dtos.User;
public class LoginUserDto
{
    [Required(ErrorMessage = "Email é obrigatório")]
    public required string Email { get; set;}
    
    [Required(ErrorMessage = "Senha é obrigatório")]
    public required string Password { get; set;}
}