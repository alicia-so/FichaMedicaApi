using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;
public class FichaMedica
{
    [Key]
    public string Id { get; set; }    
    public required string Descricao { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime DataAlteracao { get; set; }
    public required string PacienteId {get; set;}
    public virtual User Paciente {get; set;} = null!;
    public required string MedicoId {get; set;}
    public virtual User Medico {get; set;} = null!;

    public FichaMedica(){
        this.Id = Guid.NewGuid().ToString();
        this.DataCadastro = DateTime.Now;
        this.DataAlteracao = DateTime.Now;
    }
}
