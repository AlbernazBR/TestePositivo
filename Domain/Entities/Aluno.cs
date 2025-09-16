using System.ComponentModel.DataAnnotations;
using TestePositivo.Domain.Enums;

namespace TestePositivo.Domain.Entities;

public class Aluno
{
    public long Id { get; private set; }
    public long Matricula { get; private set; }
    public string NomeCompleto { get; set; } = "";
    public DateTime DataNascimento { get; set; }
    public SerieEnum Serie { get; set; }
    public SegmentoEnum Segmento { get; private set; }
    public string? NomePai { get; set; }
    public string? NomeMae { get; set; }
    public Endereco? Endereco { get; set; } = null;


    public void DefinirMatriculaSeNecessario()
    {
        if (Matricula == 0) Matricula = Id;
    }

    public void DerivarSegmento()
    {
        Segmento = Serie switch
        {
            SerieEnum.G1 or SerieEnum.G2 or SerieEnum.G3 => SegmentoEnum.Infantil,
            SerieEnum.Ano1 or SerieEnum.Ano2 or SerieEnum.Ano3 or SerieEnum.Ano4 or SerieEnum.Ano5 => SegmentoEnum.AnosIniciais,
            SerieEnum.Ano6 or SerieEnum.Ano7 or SerieEnum.Ano8 or SerieEnum.Ano9 => SegmentoEnum.AnosFinais,
            _ => SegmentoEnum.EnsinoMedio
        };
    }

    public int CalcularIdadeEmAnos(DateTime hoje)
    {
        var idade = hoje.Year - DataNascimento.Year;
        if (DataNascimento.Date > hoje.Date.AddYears(-idade))
            idade--;

        return idade;
    }

    public void ValidarIdadePorSerie(DateTime hoje)
    {
        var idade = CalcularIdadeEmAnos(hoje);
        bool ok = Serie switch
        {
            SerieEnum.G1 or SerieEnum.G2 or SerieEnum.G3 => idade is >= 3 and <= 5,
            SerieEnum.Ano1 or SerieEnum.Ano2 or SerieEnum.Ano3 or SerieEnum.Ano4 or SerieEnum.Ano5 => idade is >= 6 and <= 10,
            SerieEnum.Ano6 or SerieEnum.Ano7 or SerieEnum.Ano8 or SerieEnum.Ano9 => idade is >= 11 and <= 14,
            SerieEnum.EM1 or SerieEnum.EM2 or SerieEnum.EM3 => idade is >= 15 and <= 17,
            _ => false
        };

        if (!ok)
            throw new ValidationException($"Idade {idade} fora da faixa permitida para a série {Serie}.");
    }
}
