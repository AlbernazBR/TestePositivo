using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using TestePositivo.Models;

namespace TestePositivo.Controllers;

public class PagesController : Controller
{
    public IActionResult Privacy() => View();
    public IActionResult About()
    {
        ViewBag.Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString();
        return View();
    }

    public IActionResult ConsultasSql()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "sql", "consultas.sql");
        if (!System.IO.File.Exists(path)) return NotFound();
        var bytes = System.IO.File.ReadAllBytes(path);
        return File(bytes, "application/sql", "consultas.sql");
    }

    public IActionResult Tutorial()
    {
        return View();
    }

    public IActionResult Error()
    {
        var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

        var model = new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            Message = feature?.Error.Message,
            Path = feature?.Path,
            ExceptionType = feature?.Error.GetType().Name
        };

        return View("~/Views/Shared/Error.cshtml", model);
    }

    public IActionResult Status(int code)
    {
        ViewData["StatusCode"] = code;
        return View();
    }
}
