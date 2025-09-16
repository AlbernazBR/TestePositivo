using TestePositivo.Application.ViewModels;

namespace TestePositivo.Application.Services;

public interface IAlunosService
{
    Task<List<AlunoListVm>> ListarAsync();
    Task<AlunoEditVm?> BuscarParaEdicaoAsync(long id);
    Task<AlunoDeleteVm?> BuscarParaExclusaoAsync(long id);
    Task<AlunoDetailsVm?> BuscarDetalhesAsync(long id);
    Task<long> CriarAsync(AlunoEditVm vm, DateTime hoje);
    Task AtualizarAsync(long id, AlunoEditVm vm, DateTime hoje);
    Task RemoverAsync(long id);
}
