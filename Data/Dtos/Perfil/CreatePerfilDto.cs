using System.ComponentModel.DataAnnotations;

namespace WebApp.Data.Dtos.Perfil;

public class CreatePerfilDto
{   
    [StringLength(50,ErrorMessage = "Descricao deve ter no maximo 50 caracteres")]
    public required string Descricao { get; set; }    
}