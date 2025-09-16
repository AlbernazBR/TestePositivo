using TestePositivo.Domain.Enums;

namespace TestePositivo.Application.ViewModels;

public class AlunoDetailsVm
{
    public long Id { get; set; }
    public long Matricula { get; set; }
    public string NomeCompleto { get; set; } = "";
    public DateTime DataNascimento { get; set; }
    public SerieEnum Serie { get; set; }
    public SegmentoEnum Segmento { get; set; }

    public string? NomePai { get; set; }
    public string? NomeMae { get; set; }

    public EnderecoVm? Endereco { get; set; }
}
