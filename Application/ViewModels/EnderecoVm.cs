using System.ComponentModel.DataAnnotations;
using TestePositivo.Domain.Enums;

namespace TestePositivo.Application.ViewModels;

public class EnderecoVm
{
    public long Id { get; set; }
    public long AlunoId { get; set; }

    [Display(Name = "Tipo de Endereço")]
    [Required]
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
