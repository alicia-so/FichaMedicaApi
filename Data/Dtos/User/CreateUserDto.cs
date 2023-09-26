using System.ComponentModel.DataAnnotations;

namespace WebApp.Data.Dtos.User;

public class CreateUserDto
{
    [Required(ErrorMessage = "UserName obrigatorio")]
    [StringLength(20,ErrorMessage="UserName deve ter no maximo 20 caracteres")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "LastName obrigatorio")]
    [StringLength(100,ErrorMessage="LastName deve ter no maximo 100 caracteres")]
    public required string LastName { get; set; }

    [Required(ErrorMessage = "E-mail é obrigatorio")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Senha obrigatoria")]
    public required string Password { get; set; }

    public required string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Perfil obrigatorio")]
    public required string PerfilId { get; set; }
}