using Microsoft.AspNetCore.Mvc;
using System.Reflection;

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
}
