using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TestePositivo.Application.Services;
using TestePositivo.Application.ViewModels;

namespace TestePositivo.Controllers;

public class AlunosController(IAlunosService service) : Controller
{
    public async Task<IActionResult> Index()
    {
        var lista = await service.ListarAsync();
        return View(lista);
    }

    public async Task<IActionResult> Details(long id)
    {
        var vm = await service.BuscarDetalhesAsync(id);
        if (vm is null) return NotFound();
        return View(vm);
    }

    public IActionResult Create()
    {
        return View(new AlunoEditVm { Endereco = new() });
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AlunoEditVm vm)
    {
        if (!ModelState.IsValid) return View(vm);
        try
        {
            await service.CriarAsync(vm, DateTime.Today);
            return RedirectToAction(nameof(Index));
        }
        catch (ValidationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(vm);
        }
    }

    public async Task<IActionResult> Edit(long id)
    {
        var vm = await service.BuscarParaEdicaoAsync(id);
        if (vm is null) return NotFound();
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(long id, AlunoEditVm vm)
    {
        if (id != vm.Id) return BadRequest();
        if (!ModelState.IsValid) return View(vm);

        try
        {
            await service.AtualizarAsync(id, vm, DateTime.Today);
            return RedirectToAction(nameof(Index));
        }
        catch (ValidationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(vm);
        }
    }

    public async Task<IActionResult> Delete(long id)
    {
        var vm = await service.BuscarParaExclusaoAsync(id);
        if (vm is null) return NotFound();
        return View(vm);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        await service.RemoverAsync(id);
        TempData["Success"] = "Aluno excluído com sucesso.";
        return RedirectToAction(nameof(Index));
    }
}
