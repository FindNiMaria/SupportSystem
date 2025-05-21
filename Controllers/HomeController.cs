using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HelpdeskSystem.Models;
using Microsoft.EntityFrameworkCore;
using HelpdeskSystem.Data;
using HelpdeskSystem.Models.Ticket;
using System.Security.Claims;
using HelpdeskSystem.ViewModels;

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
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        IQueryable<Models.Ticket.Ticket> ticketQuery = _context.Tickets
            .Include(t => t.AssignedTo)
            .Include(t => t.CreatedBy)
            .Include(t => t.Status)
                .ThenInclude(s => s.SystemCode);

        IQueryable<Models.SO.OS> osQuery = _context.OS
            .Include(o => o.AssignedTo)
            .Include(o => o.CreatedBy)
            .Include(o => o.Status)
                .ThenInclude(s => s.SystemCode);

        // Verificar papel do usuário
        if (User.IsInRole("Administrador"))
        {
            // vê todos os chamados
        }
        else if (User.IsInRole("Técnico"))
        {
            ticketQuery = ticketQuery.Where(t => t.AssignedToId == userId);
            osQuery = osQuery.Where(o => o.AssignedToId == userId);
        }
        else if (User.IsInRole("Coordenador"))
        {
            // Aqui seria necessário saber o Departamento do usuário
            var departamentoId = await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => u.DepartmentId)
                .FirstOrDefaultAsync();

            ticketQuery = ticketQuery.Where(t => t.CreatedBy.DepartmentId == departamentoId);
            osQuery = osQuery.Where(o => o.CreatedBy.DepartmentId == departamentoId);
        }
        else
        {
            // Usuário comum
            ticketQuery = ticketQuery.Where(t => t.CreatedById == userId);
            osQuery = osQuery.Where(o => o.CreatedById == userId);
        }

        var tickets = await ticketQuery
            .Select(t => new TicketOsViewModel
            {
                Id = t.Id,
                Title = t.Title,
                Type = "Ticket",
                StatusCode = t.Status.Description,
                StatusSystemCode = t.Status.SystemCode.Code,
                CreatedOn = t.CreatedOn,
                AssignedToName = t.AssignedTo != null ? t.AssignedTo.FirstName : "Não atribuído"
            }).ToListAsync();

        var ordens = await osQuery
            .Select(o => new TicketOsViewModel
            {
                Id = o.Id,
                Title = o.Title,
                Type = "OS",
                StatusCode = o.Status.Description,
                StatusSystemCode = o.Status.SystemCode.Code,
                CreatedOn = o.CreatedOn,
                AssignedToName = o.AssignedTo != null ? o.AssignedTo.FirstName : "Não atribuído"
            }).ToListAsync();

        var todos = tickets.Concat(ordens).OrderByDescending(x => x.CreatedOn).ToList();

        // Contagem separada
        ViewBag.ticketsFechados = tickets.Count(x => x.StatusSystemCode == "STS" && x.StatusCode == "Concluído");
        ViewBag.ticketsAbertos = tickets.Count(x => x.StatusSystemCode == "STS" && x.StatusCode == "Pendente");

        ViewBag.osFechadas = ordens.Count(x => x.StatusSystemCode == "STS" && x.StatusCode == "Concluído");
        ViewBag.osAbertas = ordens.Count(x => x.StatusSystemCode == "STS" && x.StatusCode == "Pendente");

        return View(todos);
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
