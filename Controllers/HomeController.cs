using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HelpdeskSystem.Models;
using Microsoft.EntityFrameworkCore;
using HelpdeskSystem.Data;

namespace HelpdeskSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger,ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.totalFechado = _context.Tickets.Count(x => x.Status.SystemCode.Codigo == "Status" && x.Status.Codigo == "Fechado");

        ViewBag.totalAberto = _context.Tickets.Count(x => x.Status.SystemCode.Codigo == "Status" && x.Status.Codigo == "Pendente");


        var tickets = await _context.Tickets
                .Include(t => t.CriadoPor)
                .OrderBy(x => x.CriadoEm)
                .ToListAsync();

        return View(tickets);

    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
