using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpdeskSystem.Data;
using HelpdeskSystem.Models;
using System.Security.Claims;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using HelpdeskSystem.ViewModels;
using System.Net.Mail;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit;
using HelpdeskSystem.Services;

namespace HelpdeskSystem.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailTicketService _emailTicketService;
        public TicketsController(ApplicationDbContext context, IConfiguration configuration, IEmailTicketService emailTicketService)
        {

            _context = context;
            _configuration = configuration;
            _emailTicketService = emailTicketService;
        }

        public async Task<IActionResult> ImportEmails()
        {
            await _emailTicketService.ImportarEmailsComoTicketsAsync();
            return RedirectToAction("Index");
        }
        // GET: Tickets
        public async Task<IActionResult> Index(TicketViewModel vm)
        {
            vm.Tickets = await _context.Tickets
                .Include(t => t.CriadoPor)
                .Include(t => t.SubCategory)
                .Include(t => t.Status)
                .Include(t => t.Prioridade)
                .Include(t => t.TicketComments)
                .OrderBy(x => x.CriadoEm)
                .ToListAsync();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int id, TicketViewModel vm)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Comment newcomment = new();
            newcomment.IdChamado = id;
            newcomment.CriadoPorId = userId;
            newcomment.CriadoEm = DateTime.Now;
            newcomment.Descricao = vm.DescricaoComentario;
            _context.Add(newcomment);
            await _context.SaveChangesAsync();
            //Registrar no Log de Auditoria

            var activity = new AuditTrail
            {
                Action = "Criar",
                TimeStamp = DateTime.Now,
                IpAdress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserId = userId,
                Module = "Comentário",
                AffectedTable = "Comentário"
            };
            _context.Add(activity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = id });
        }
        // GET: Tickets/Details/5

        public async Task<IActionResult> Details(int id, TicketViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.DetalhesChamado = await _context.Tickets
            .Include(t => t.CriadoPor)
            .Include(t => t.SubCategory)
            .Include(t => t.Status)
            .Include(t => t.Prioridade)
            .FirstOrDefaultAsync(m => m.Id == id);

            vm.ObservacoesChamado = await _context.Comment
                .Include(t => t.CriadoPor)
                .Include(t => t.Ticket)
                .Where(t => t.IdChamado == id)
                .ToListAsync();
            if (vm.DetalhesChamado == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            ViewData["PrioridadeId"] = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Codigo == "Prioridade"), "Id", "Descricao");
            ViewData["CriadoPorId"] = new SelectList(_context.Users, "Id", "FullName");
            ViewData["CategoriaId"] = new SelectList(_context.TicketCategories, "Id", "Nome");
            ViewBag.Categorias = new SelectList(_context.TicketCategories, "Id", "Nome");
            ViewBag.Prioridades = new SelectList(_context.systemCodeDetails
                .Include(x => x.SystemCode)
                .Where(x => x.SystemCode.Codigo == "Prioridade"), "Id", "Descricao");
            return View();
        }

        // POST: Tickets/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketViewModel ticketvm, IFormFile anexo)
        {
            if (anexo.Length > 0)
            {
                var filename = "TicketAttachment" + DateTime.Now.ToString("yyyymmddhhmmss");
                var path = _configuration["FileSettings:UploadsFolder"]!;
                var filepath = Path.Combine(path, filename);
                var stream = new FileStream(filepath, FileMode.Create);
                await anexo.CopyToAsync(stream);
                ticketvm.Anexo = filename;
            }
            var pending = await _context.systemCodeDetails
                .Include(x => x.SystemCode)
                .Where(x => x.SystemCode.Codigo == "Status" && x.Codigo == "Pendente")
                .FirstOrDefaultAsync();

            Ticket ticket = new();
            var categoria = await _context.TicketCategories.FindAsync(ticketvm.CategoriaId);
            if (categoria != null && categoria.PrioridadePadraoId.HasValue)
            {
                ticket.PrioridadeId = categoria.PrioridadePadraoId.Value;
            }
            ticket.CategoriaId = ticketvm.CategoriaId;


            ticket.Id = ticketvm.Id;
            ticket.Titulo = ticketvm.Titulo;
            ticket.Descricao = ticketvm.Descricao;
            ticket.StatusId = pending.Id;
            ticket.SubCategoryId = ticketvm.SubCategoriaId;
            ticket.Anexo = ticketvm.Anexo;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ticket.CriadoEm = DateTime.Now;
            ticket.CriadoPorId = userId;
            _context.Add(ticket);
            await _context.SaveChangesAsync();

            //Registrar no Log de Auditoria

            var activity = new AuditTrail
            {
                Action = "Criar",
                TimeStamp = DateTime.Now,
                IpAdress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserId = userId,
                Module = "Chamados",
                AffectedTable = "Tickets"
            };
            _context.Add(activity);
            await _emailTicketService.EnviarEmailAsync(
                    para: User.FindFirstValue(ClaimTypes.Email),
                    assunto: $"Chamado nº {ticket.Id} criado",
                    mensagem: $"Seu chamado \"{ticket.Titulo}\" foi criado com sucesso e está com status Pendente."
);

            await _context.SaveChangesAsync();
            ViewData["CriadoPorId"] = new SelectList(_context.Users, "Id", "FullName", ticket.CriadoPorId);
            ViewData["CategoriaId"] = new SelectList(_context.TicketCategories, "Id", "Nome");
            ViewData["PrioridadeId"] = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Codigo == "Prioridade"), "Id", "Descricao");
            return RedirectToAction(nameof(Index));
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var ticket = await _context.Tickets
                .Include(t => t.Categoria)
                .Include(t => t.SubCategory)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (ticket == null)
                return NotFound();

            ViewBag.Categorias = new SelectList(_context.TicketCategories, "Id", "Nome", ticket.CategoriaId);

            ViewBag.Prioridades = new SelectList(
                _context.systemCodeDetails
                    .Include(x => x.SystemCode)
                    .Where(x => x.SystemCode.Codigo == "Prioridade"),
                "Id",
                "Descricao",
                ticket.PrioridadeId
            );

            ViewBag.SubCategorias = new SelectList(
                _context.TicketSubCategories
                    .Where(x => x.CategoriaId == ticket.CategoriaId),
                "Id",
                "Name",
                ticket.SubCategoryId
            );

            ViewData["CriadoPorId"] = new SelectList(_context.Users, "Id", "FullName", ticket.CriadoPorId);

            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ticket ticket)
        {
            if (id != ticket.Id)
                return NotFound();

            var ticketOriginal = await _context.Tickets
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (ticketOriginal == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Preserva os dados originais de criação
            ticket.CriadoEm = ticketOriginal.CriadoEm;
            ticket.CriadoPorId = ticketOriginal.CriadoPorId;

            // Marca como modificado
            ticket.ModificadoEm = DateTime.Now;
            ticket.ModificadoPorId = userId;

            if (ModelState.IsValid)
            {
                _context.Update(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(ticket);
        }



        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.CriadoPor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
