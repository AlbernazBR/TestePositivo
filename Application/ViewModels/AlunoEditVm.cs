using System.ComponentModel.DataAnnotations;
using TestePositivo.Domain.Enums;

namespace TestePositivo.Application.ViewModels;

public class AlunoEditVm
{
    public long Id { get; set; }

    [Display(Name = "Nome completo")]
    [Required, StringLength(200)]
    public string NomeCompleto { get; set; } = "";

    [Display(Name = "Data de Nascimento")]
    [DataType(DataType.Date)]
    [Required]
    public DateTime DataNascimento { get; set; }

    [Required]
    public SerieEnum Serie { get; set; }

    [Display(Name = "Nome do Pai")]
    [StringLength(200)]
    public string? NomePai { get; set; }

    [Display(Name = "Nome da Mãe")]
    [StringLength(200)]
    public string? NomeMae { get; set; }

    [Required]
    public EnderecoVm Endereco { get; set; } = new();
}
