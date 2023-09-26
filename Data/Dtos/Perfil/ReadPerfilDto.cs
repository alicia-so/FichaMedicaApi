namespace WebApp.Data.Dtos.Perfil;
public class ReadPerfilDto
{
    public required string Id { get; set; }
    public required string Descricao { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime DataAlteracao { get; set; }
}
