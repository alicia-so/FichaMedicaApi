using System.ComponentModel.DataAnnotations;
using WebApp.Data.Dtos.Perfil;

namespace WebApp.Data.Dtos.User;
public class ReadUserDto
{
    [Key]
    public required string Id { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required virtual ReadPerfilDto Perfis { get; set; }
}