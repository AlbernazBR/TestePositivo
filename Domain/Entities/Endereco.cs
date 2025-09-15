using System.ComponentModel.DataAnnotations;
using TestePositivo.Domain.Enums;

namespace TestePositivo.Domain.Entities;

public class Endereco
{
    public long Id { get; private set; }
    public long AlunoId { get; set; }

    [Display(Name = "Tipo de Endereço")]
    public TipoEnderecoEnum Tipo { get; set; }

    [Required, StringLength(200)]
    public string Rua { get; set; } = "";

    [Required, StringLength(20)]
    public string CEP { get; set; } = "";

    [Required, StringLength(20)]
    public string Numero { get; set; } = "";

    [StringLength(100)]
    public string? Complemento { get; set; }
}