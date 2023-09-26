using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;
public class Perfil
{
    [Key]
    public string Id { get; set; }
    
    [StringLength(50,ErrorMessage = "Descricao deve ter no maximo 50 caracteres")]
    public required string Descricao { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime DataAlteracao { get; set; }

    public Perfil(){
        this.Id = Guid.NewGuid().ToString();
        this.DataCadastro = DateTime.Now;
        this.DataAlteracao = DateTime.Now;
    }
}
