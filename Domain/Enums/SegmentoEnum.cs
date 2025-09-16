using System.ComponentModel.DataAnnotations;

namespace TestePositivo.Domain.Enums;

public enum SegmentoEnum
{
    [Display(Name = "Infantil")]
    Infantil = 1,

    [Display(Name = "Anos Iniciais")]
    AnosIniciais = 2,

    [Display(Name = "Anos Finais")]
    AnosFinais = 3,

    [Display(Name = "Ensino Médio")]
    EnsinoMedio = 4
}
