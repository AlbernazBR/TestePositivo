using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TestePositivo.Application.ViewModels;
using TestePositivo.Data;
using TestePositivo.Domain.Entities;

namespace TestePositivo.Application.Services;

public class AlunosService(AppDbContext db, IMapper mapper) : IAlunosService
{
    public async Task<List<AlunoListVm>> ListarAsync()
    {
        return await db.Alunos
            .Include(a => a.Endereco)
            .ProjectTo<AlunoListVm>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<AlunoEditVm?> BuscarParaEdicaoAsync(long id)
    {
        var aluno = await db.Alunos.Include(a => a.Endereco).FirstOrDefaultAsync(a => a.Id == id);
        return aluno is null ? null : mapper.Map<AlunoEditVm>(aluno);
    }

    public async Task<AlunoDeleteVm?> BuscarParaExclusaoAsync(long id)
    {
        return await db.Alunos
            .Where(a => a.Id == id)
            .Select(a => new AlunoDeleteVm
            {
                Id = a.Id,
                Matricula = a.Matricula,
                NomeCompleto = a.NomeCompleto,
                Serie = a.Serie,
                Segmento = a.Segmento
            })
            .FirstOrDefaultAsync();
    }

    public async Task<AlunoDetailsVm?> BuscarDetalhesAsync(long id)
    {
        return await db.Alunos
            .Where(a => a.Id == id)
            .Select(a => new AlunoDetailsVm
            {
                Id = a.Id,
                Matricula = a.Matricula,
                NomeCompleto = a.NomeCompleto,
                DataNascimento = a.DataNascimento,
                Serie = a.Serie,
                Segmento = a.Segmento,
                NomePai = a.NomePai,
                NomeMae = a.NomeMae,

                Endereco = a.Endereco == null ? null : new EnderecoVm
                {
                    Id = a.Endereco.Id,
                    AlunoId = a.Endereco.AlunoId,
                    Tipo = a.Endereco.Tipo,
                    Rua = a.Endereco.Rua,
                    CEP = a.Endereco.CEP,
                    Numero = a.Endereco.Numero,
                    Complemento = a.Endereco.Complemento
                }
            })
            .FirstOrDefaultAsync();
    }


    public async Task<long> CriarAsync(AlunoEditVm vm, DateTime hoje)
    {
        var aluno = mapper.Map<Aluno>(vm);
        aluno.DerivarSegmento();
        aluno.ValidarIdadePorSerie(hoje);

        db.Alunos.Add(aluno);
        await db.SaveChangesAsync();

        aluno.DefinirMatriculaSeNecessario();
        await db.SaveChangesAsync();

        return aluno.Id;
    }

    public async Task AtualizarAsync(long id, AlunoEditVm vm, DateTime hoje)
    {
        var aluno = await db.Alunos.Include(a => a.Endereco).FirstOrDefaultAsync(a => a.Id == id);
        if (aluno is null) throw new KeyNotFoundException("Aluno não encontrado.");

        mapper.Map(vm, aluno);
        aluno.DerivarSegmento();
        aluno.ValidarIdadePorSerie(hoje);

        await db.SaveChangesAsync();
    }

    public async Task RemoverAsync(long id)
    {
        var aluno = await db.Alunos.Include(a => a.Endereco).FirstOrDefaultAsync(a => a.Id == id);
        if (aluno is null) return;
        db.Remove(aluno);
        await db.SaveChangesAsync();
    }
}
