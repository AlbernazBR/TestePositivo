using System.ComponentModel.DataAnnotations;
using TestePositivo.Domain.Enums;

namespace TestePositivo.Application.ViewModels;

public class EnderecoVm
{
    public long Id { get; set; }
    public long AlunoId { get; set; }

    [Display(Name = "Tipo de Endereço")]
    [Range(1, int.MaxValue, ErrorMessage = "Selecione o tipo")]
    [Required(ErrorMessage = "O campo Tipo é obrigatório.")]
    public TipoEnderecoEnum? Tipo { get; set; }

    [StringLength(200)]
    [Required(ErrorMessage = "O campo Rua é obrigatório.")]
    public string Rua { get; set; } = "";

    [Display(Name = "CEP")]
    [Required(ErrorMessage = "O campo CEP é obrigatório.")]
    [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "O CEP deve estar no formato 00000-000.")]
    public string CEP { get; set; } = "";

    [StringLength(20)]
    [Required(ErrorMessage = "O campo Número é obrigatório.")]
    public string Numero { get; set; } = "";

    [StringLength(100)]
    public string? Complemento { get; set; }
}
