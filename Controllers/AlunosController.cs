using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TestePositivo.Data;
using TestePositivo.Domain.Entities;
using TestePositivo.Domain.Enums;

namespace TestePositivo.Controllers;

public class AlunosController(AppDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        var alunos = await db.Alunos.Include(a => a.Endereco).ToListAsync();
        return View(alunos);
    }

    public IActionResult Create()
    {
        return View(new Aluno
        {
            Endereco = new Endereco { Tipo = TipoEnderecoEnum.Residencial }
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Aluno model)
    {
        if (!ModelState.IsValid) return View(model);

        model.DerivarSegmento();

        try
        {
            model.ValidarIdadePorSerie(DateTime.Today);
        }
        catch (ValidationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }

        db.Alunos.Add(model);
        await db.SaveChangesAsync();

        model.DefinirMatriculaSeNecessario();
        await db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(long id)
    {
        var aluno = await db.Alunos.Include(a => a.Endereco).FirstOrDefaultAsync(a => a.Id == id);
        if (aluno is null) return NotFound();
        return View(aluno);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(long id, Aluno model)
    {

        if (!ModelState.IsValid) return View(model);

        var aluno = await db.Alunos
            .Include(a => a.Endereco)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (aluno is null) return NotFound();

        aluno.NomeCompleto = model.NomeCompleto;
        aluno.DataNascimento = model.DataNascimento;
        aluno.Serie = model.Serie;
        aluno.DerivarSegmento();

        aluno.NomePai = model.NomePai;
        aluno.NomeMae = model.NomeMae;

        aluno.Endereco.Tipo = model.Endereco.Tipo;
        aluno.Endereco.Rua = model.Endereco.Rua;
        aluno.Endereco.CEP = model.Endereco.CEP;
        aluno.Endereco.Numero = model.Endereco.Numero;
        aluno.Endereco.Complemento = model.Endereco.Complemento;

        try
        {
            aluno.ValidarIdadePorSerie(DateTime.Today);
        }
        catch (ValidationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }

        await db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Details(long id)
    {
        var aluno = await db.Alunos.Include(a => a.Endereco).FirstOrDefaultAsync(a => a.Id == id);
        if (aluno is null) return NotFound();
        return View(aluno);
    }

    public async Task<IActionResult> Delete(long id)
    {
        var aluno = await db.Alunos.Include(a => a.Endereco).FirstOrDefaultAsync(a => a.Id == id);
        if (aluno is null) return NotFound();
        return View(aluno);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        var aluno = await db.Alunos.Include(a => a.Endereco).FirstOrDefaultAsync(a => a.Id == id);
        if (aluno is null) return NotFound();

        db.Remove(aluno);
        await db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
