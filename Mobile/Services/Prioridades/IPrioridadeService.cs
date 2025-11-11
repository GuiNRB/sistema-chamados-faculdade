using SistemaChamados.Shared.DTOs;

namespace SistemaChamados.Mobile.Services.Prioridades;

public interface IPrioridadeService
{
    Task<IEnumerable<PrioridadeDto>?> GetAll();
}
