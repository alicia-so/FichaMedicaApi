namespace WebApp.Data.Dtos.FichaMedica;
public class CreateFichaMedicaDto
{
    public required string PacienteId {get; set;}
    public required string MedicoId {get; set;}
    public required string Descricao { get; set; }
}
