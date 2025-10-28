namespace SistemaChamados.Application.DTOs;

public class ChamadoListDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string CategoriaNome { get; set; } = string.Empty;
    public string StatusNome { get; set; } = string.Empty;
}