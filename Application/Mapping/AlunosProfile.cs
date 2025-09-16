using AutoMapper;
using TestePositivo.Application.ViewModels;
using TestePositivo.Domain.Entities;

namespace TestePositivo.Application.Mapping;

public class AlunosProfile : Profile
{
    public AlunosProfile()
    {
        CreateMap<Endereco, EnderecoVm>().ReverseMap();

        CreateMap<Aluno, AlunoEditVm>()
            .ForMember(d => d.Endereco, o => o.MapFrom(s => s.Endereco));

        CreateMap<AlunoEditVm, Aluno>()
            .ForMember(d => d.Matricula, o => o.Ignore())
            .ForMember(d => d.Segmento, o => o.Ignore())
            .ForMember(d => d.Endereco, o => o.MapFrom(s => s.Endereco));

        CreateMap<Aluno, AlunoListVm>()
            .ForMember(d => d.EnderecoResumo, o => o.MapFrom(s =>
                s.Endereco == null ? null : $"{s.Endereco.Rua}, {s.Endereco.Numero} - CEP {s.Endereco.CEP}"));
    }
}
