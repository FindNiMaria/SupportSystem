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
using HelpdeskSystem.Models.Ticket;
using HelpdeskSystem.Models.User;


namespace HelpdeskSystem.Controllers.Chamados
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
                .Include(t => t.CreatedBy)
                .Include(t => t.SubCategory)
                .Include(t => t.Status)
                .Include(t => t.Priority)
                .Include(t => t.TicketComments)
                .OrderBy(x => x.CreatedOn)
                .ToListAsync();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int id, TicketViewModel vm)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Comment newcomment = new();
            newcomment.TicketId = id;
            newcomment.CreatedById = userId;
            newcomment.CreatedOn = DateTime.Now;
            newcomment.Description = vm.TicketDescription;
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
            return RedirectToAction("Details", new { id });
        }
        //POST: Add status resolved
        [HttpPost]
        public async Task<IActionResult> ResolvedConfirmed(int id, TicketViewModel vm)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            TicketResolution resolution = new();
            resolution.TicketId = id;
            resolution.CreatedById = userId;
            resolution.CreatedOn = DateTime.Now;
            resolution.Description = vm.TicketDescription;
            resolution.StatusId = vm.StatusId;
            _context.Add(resolution);
            await _context.SaveChangesAsync();

            var ticket = await _context.Tickets
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            ticket.StatusId = vm.StatusId;
            _context.Update(ticket);
            //Registrar no Log de Auditoria

            var activity = new AuditTrail
            {
                Action = "Mudança de Status",
                TimeStamp = DateTime.Now,
                IpAdress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserId = userId,
                Module = "Chamados",
                AffectedTable = "Tickets"
            };
            _context.Add(activity);
            var client = await _context.Users.FirstOrDefaultAsync(u => u.Id == resolution.CreatedById);
            var status = await _context.systemCodeDetails.FindAsync(resolution.StatusId);
            await _emailTicketService.EnviarEmailAsync(
                    para: client.Email,
                    assunto: $"O Chamado nº {resolution.TicketId} recebeu atualizações",
                    mensagem: $"Seu chamado \"{resolution.TicketId}\" foi atualizado por um técnico com sucesso e está com status \"{status?.Description}\".\n\n\"{resolution.Description}\"");

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: Tickets/Details/5

        public async Task<IActionResult> Details(int id, TicketViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.TicketDetails = await _context.Tickets
            .Include(t => t.CreatedBy)
            .Include(t => t.SubCategory)
            .Include(t => t.Status)
            .Include(t => t.Priority)
            .FirstOrDefaultAsync(m => m.Id == id);

            vm.TicketComments = await _context.Comment
                .Include(t => t.CreatedBy)
                .Include(t => t.Ticket)
                .Where(t => t.TicketId == id)
                .ToListAsync();
            if (vm.TicketDetails == null)
            {
                return NotFound();
            }

            return View(vm);
        }
        //GET: Ticket Resolve
        public async Task<IActionResult> Resolve(int id, TicketViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.TicketDetails = await _context.Tickets
            .Include(t => t.CreatedBy)
            .Include(t => t.SubCategory)
            .Include(t => t.Status)
            .Include(t => t.Priority)
            .FirstOrDefaultAsync(m => m.Id == id);

            vm.TicketResolutions = await _context.TicketResolutions
            .Include(t => t.CreatedBy)
            .Include(t => t.Ticket)
            .Include(t => t.Status)
            .Where(t => t.TicketId == id)
            .ToListAsync();

            vm.TicketComments = await _context.Comment
                .Include(t => t.CreatedBy)
                .Include(t => t.Ticket)
                .Where(t => t.TicketId == id)
                .ToListAsync();
            if (vm.TicketDetails == null)
            {
                return NotFound();
            }
            ViewData["StatusId"] = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "STS"), "Id", "Description");
            return View(vm);
        }

        //GET: Ticket Assignment
        [HttpGet]
        public async Task<IActionResult> TicketAssignment(int id, TicketViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.TicketDetails = await _context.Tickets
            .Include(t => t.CreatedBy)
            .Include(t => t.SubCategory)
            .Include(t => t.Status)
            .Include(t => t.Priority)
            .FirstOrDefaultAsync(m => m.Id == id);

            vm.TicketResolutions = await _context.TicketResolutions
            .Include(t => t.CreatedBy)
            .Include(t => t.Ticket)
            .Include(t => t.Status)
            .Where(t => t.TicketId == id)
            .ToListAsync();

            vm.TicketComments = await _context.Comment
                .Include(t => t.CreatedBy)
                .Include(t => t.Ticket)
                .Where(t => t.TicketId == id)
                .ToListAsync();
            if (vm.TicketDetails == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName");
            return View(vm);
        }
        //POST: Add status resolved
        [HttpPost]
        public async Task<IActionResult> AssignmentConfirmed(int id, TicketViewModel vm)
        {

            var assignstatus  = await _context.systemCodeDetails
                .Include(x => x.SystemCode)
                .Where(x => x.SystemCode.Code == "STS" && x.Code == "ATB")
                .FirstOrDefaultAsync();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            TicketResolution resolution = new();
            resolution.TicketId = id;
            resolution.CreatedById = userId;
            resolution.CreatedOn = DateTime.Now;
            resolution.StatusId = assignstatus.Id;
            resolution.Description = "Chamado Atribuído a um Técnico";
            _context.Add(resolution);
            await _context.SaveChangesAsync();

            var ticket = await _context.Tickets
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            ticket.StatusId = assignstatus.Id;
            ticket.AssignedToId = vm.AssignedToId;
            ticket.AssignedOn = DateTime.Now;
            _context.Update(ticket);
            //Registrar no Log de Auditoria

            var activity = new AuditTrail
            {
                Action = "Atribuição",
                TimeStamp = DateTime.Now,
                IpAdress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserId = userId,
                Module = "Chamados",
                AffectedTable = "Tickets"
            };
            _context.Add(activity);
            var client = await _context.Users.FirstOrDefaultAsync(u => u.Id == resolution.CreatedById);
            var status = await _context.systemCodeDetails.FindAsync(resolution.StatusId);
            await _emailTicketService.EnviarEmailAsync(
                    para: client.Email,
                    assunto: $"O Chamado nº {resolution.TicketId} foi atribuído a um técnico",
                    mensagem: $"Seu chamado \"{resolution.TicketId}\"  foi atribuído a um técnico com sucesso, por favor aguarde retorno.");

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: Tickets/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["PriorityId"] = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "PRD"), "Id", "Description");
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "FullName");
            ViewData["CategoryId"] = new SelectList(_context.TicketCategories, "Id", "Name");
            ViewBag.Categorias = new SelectList(_context.TicketCategories, "Id", "Name");
            ViewBag.Prioridades = new SelectList(_context.systemCodeDetails
                .Include(x => x.SystemCode)
                .Where(x => x.SystemCode.Code == "Priority"), "Id", "Description");
            return View();
        }

        // POST: Tickets/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketViewModel ticketvm, IFormFile anexo)
        {
            if (anexo?.Length > 0)
            {
                var filename = "TicketAttachment" + DateTime.Now.ToString("yyyymmddhhmmss");
                var path = _configuration["FileSettings:UploadsFolder"]!;
                var filepath = Path.Combine(path, filename);
                var stream = new FileStream(filepath, FileMode.Create);
                await anexo.CopyToAsync(stream);
                ticketvm.Attachment = filename;
            }
            var pending = await _context.systemCodeDetails
                .Include(x => x.SystemCode)
                .Where(x => x.SystemCode.Code == "STS" && x.Code == "PND")
                .FirstOrDefaultAsync();

            Ticket ticket = new();
            var categoria = await _context.TicketCategories.FindAsync(ticketvm.CategoryId);
            if (categoria != null && categoria.DefaultPriorityId.HasValue)
            {
                ticket.PriorityId = categoria.DefaultPriorityId.Value;
            }
            ticket.CategoryId = ticketvm.CategoryId;


            ticket.Id = ticketvm.Id;
            ticket.Title = ticketvm.Title;
            ticket.Description = ticketvm.Description;
            ticket.StatusId = pending.Id;
            ticket.SubCategoryId = ticketvm.SubCategoryId;
            ticket.Attachment = ticketvm.Attachment;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ticket.CreatedOn = DateTime.Now;
            ticket.CreatedById = userId;
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
                    mensagem: $"Seu chamado \"{ticket.Title}\" foi criado com sucesso e está com status Pendente."
);

            await _context.SaveChangesAsync();
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "FullName", ticket.CreatedBy);
            ViewData["CategoryId"] = new SelectList(_context.TicketCategories, "Id", "Name");
            ViewData["PriorityId"] = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "PRD"), "Id", "Description");
            return RedirectToAction(nameof(Index));
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var ticket = await _context.Tickets
                .Include(t => t.Category)
                .Include(t => t.SubCategory)
                .Include(t => t.Priority)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (ticket == null)
                return NotFound();

            ViewBag.CategoryId = new SelectList(_context.TicketCategories, "Id", "Name", ticket.CategoryId);
            ViewBag.SubCategoryId = new SelectList(_context.TicketSubCategories.Where(x => x.CategoryId == ticket.CategoryId),"Id", "Name", ticket.SubCategoryId);
            ViewBag.PriorityId = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "PRD"),"Id","Description",ticket.PriorityId);

            

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

            var ticketOriginal = await _context.Tickets.FindAsync(id);

            if (ticketOriginal == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            // Preserva os dados originais de criação
            _context.Entry(ticketOriginal).Property(x => x.CreatedById).IsModified = false;
            _context.Entry(ticketOriginal).Property(x => x.CreatedOn).IsModified = false;
            _context.Entry(ticketOriginal).Property(x => x.StatusId).IsModified = false;

            // Marca como modificado
            ticketOriginal.ModifiedOn = DateTime.Now;
            ticketOriginal.ModifiedById = userId;
            ticketOriginal.Id = ticket.Id;
            ticketOriginal.Title = ticket.Title;
            ticketOriginal.Description = ticket.Description;
            ticketOriginal.CategoryId = ticket.CategoryId;
            ticketOriginal.SubCategoryId = ticket.SubCategoryId;
            ticketOriginal.Attachment = ticket.Attachment;



            _context.Update(ticketOriginal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }



        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.CreatedBy)
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
