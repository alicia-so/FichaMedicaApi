using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class User
{
    [Key]
    public string Id { get; set; }

    [Required]
    [StringLength(100,ErrorMessage="UserName deve ter no maximo 100 caracteres")]
    public required string Name { get; set; } = "";

    [Required]
    [StringLength(100,ErrorMessage="LastName deve ter no maximo 100 caracteres")]
    public required string LastName { get; set; } = "";

    public string FullName {get; set;}

    [Required(ErrorMessage = "E-mail é obrigatorio")] 
    public required string Email { get; set; }

    public required string Password { get; set; }

    public required string PhoneNumber { get; set; }
    
    public required string PerfilId { get; set; }
    
    public virtual Perfil Perfis { get; set; } = null!;
    
    public User()
    {
        this.Id = Guid.NewGuid().ToString();
        this.FullName = this.Name + " " + this.LastName;
    }

}