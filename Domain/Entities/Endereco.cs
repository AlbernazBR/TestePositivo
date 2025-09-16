using TestePositivo.Domain.Enums;

namespace TestePositivo.Domain.Entities;

public class Endereco
{
    public long Id { get; private set; }
    public long AlunoId { get; set; }
    public TipoEnderecoEnum Tipo { get; set; }
    public string Rua { get; set; } = "";
    public string CEP { get; set; } = "";
    public string Numero { get; set; } = "";
    public string? Complemento { get; set; }
}