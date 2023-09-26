using System.ComponentModel.DataAnnotations;
using WebApp.Data.Dtos.User;

namespace WebApp.Data.Dtos.FichaMedica;
public class ReadFichaMedicaDto
{
    public required string Id {get; set;}
    public required string PacienteId {get; set;}
    public required string MedicoId {get; set;}
    public required string Descricao { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime DataAlteracao { get; set; }
    public virtual ReadUserDto Paciente {get; set;} = null!;
    public virtual ReadUserDto Medico {get; set;} = null!;
}
