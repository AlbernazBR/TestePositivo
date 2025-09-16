using TestePositivo.Domain.Enums;

namespace TestePositivo.Application.ViewModels;

public class AlunoDeleteVm
{
    public long Id { get; set; }
    public long Matricula { get; set; }
    public string NomeCompleto { get; set; } = "";
    public SerieEnum Serie { get; set; }
    public SegmentoEnum Segmento { get; set; }
}
