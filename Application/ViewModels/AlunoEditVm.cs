using System.ComponentModel.DataAnnotations;
using TestePositivo.Domain.Enums;

namespace TestePositivo.Application.ViewModels;

public class AlunoEditVm
{
    public long Id { get; set; }

    [Display(Name = "Nome completo")]
    [StringLength(200)]
    [Required(ErrorMessage = "O campo Nome completo é obrigatório.")]
    public string NomeCompleto { get; set; } = "";

    [Display(Name = "Data de Nascimento")]
    [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório.")]
    [DataType(DataType.Date)]
    public DateTime? DataNascimento { get; set; }

    [Required(ErrorMessage = "O campo Série é obrigatório.")]
    public SerieEnum? Serie { get; set; }

    [Display(Name = "Nome do Pai")]
    [StringLength(200)]
    public string? NomePai { get; set; }

    [Display(Name = "Nome da Mãe")]
    [StringLength(200)]
    public string? NomeMae { get; set; }

    [Required]
    public EnderecoVm Endereco { get; set; } = new();
}
