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

namespace HelpdeskSystem.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index(TicketViewModel vm)
        {
            vm.Tickets = await _context.Tickets
                .Include(t => t.CriadoPor)
                .Include(t=> t.SubCategory)
                .OrderBy(x => x.CriadoEm)
                .ToListAsync();

            return View(vm);
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Tickets/Create
        public IActionResult Create()
        {
            ViewData["PrioridadeId"] = new SelectList(_context.systemCodeDetails.Include(x=> x.SystemCode).Where(x => x.SystemCode.Codigo == "Prioridade"), "Id", "Descricao");
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
        public async Task<IActionResult> Create( TicketViewModel ticketvm)
        {
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
