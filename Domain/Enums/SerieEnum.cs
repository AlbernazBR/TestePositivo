using System.ComponentModel.DataAnnotations;

namespace TestePositivo.Domain.Enums;

public enum SerieEnum
{
    [Display(Name = "G1")]
    G1 = 1,
    [Display(Name = "G2")]
    G2 = 2,
    [Display(Name = "G3")]
    G3 = 3,

    [Display(Name = "1º Ano")]
    Ano1 = 11,
    [Display(Name = "2º Ano")]
    Ano2 = 12,
    [Display(Name = "3º Ano")]
    Ano3 = 13,
    [Display(Name = "4º Ano")]
    Ano4 = 14,
    [Display(Name = "5º Ano")]
    Ano5 = 15,

    [Display(Name = "6º Ano")]
    Ano6 = 21,
    [Display(Name = "7º Ano")]
    Ano7 = 22,
    [Display(Name = "8º Ano")]
    Ano8 = 23,
    [Display(Name = "9º Ano")]
    Ano9 = 24,

    [Display(Name = "1º Ano EM")]
    EM1 = 31,
    [Display(Name = "2º Ano EM")]
    EM2 = 32,
    [Display(Name = "3º Ano EM")]
    EM3 = 33
}
