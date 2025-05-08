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
using HelpdeskSystem.Models.OS;
using HelpdeskSystem.Models.User;
using HelpdeskSystem.Data.Migrations;


namespace HelpdeskSystem.Controllers.Os
{
    public class OSController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailTicketService _emailTicketService;


        public OSController(ApplicationDbContext context, IConfiguration configuration, IEmailTicketService emailTicketService)
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
        public async Task<IActionResult> Index(OSViewModel vm)
        {
            vm.OS = await _context.OS
                .Include(t => t.CreatedBy)
                .Include(t => t.SubCategory)
                .Include(t => t.Status)
                .Include(t => t.Priority)
                .Include(t => t.OSComments)
                .OrderBy(x => x.CreatedOn)
                .ToListAsync();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int id, OSViewModel vm)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            OSComment newcomment = new();
            newcomment.OSId = id;
            newcomment.CreatedById = userId;
            newcomment.CreatedOn = DateTime.Now;
            newcomment.Description = vm.OSDescription;
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
                AffectedTable = "OS"
            };
            _context.Add(activity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id });
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> ResolvedConfirmed(int id, OSViewModel vm)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            OSResolution resolution = new();
            resolution.OSId = id;
            resolution.CreatedById = userId;
            resolution.CreatedOn = DateTime.Now;
            resolution.Description = vm.OSDescription;
            resolution.StatusId = vm.StatusId;
            _context.Add(resolution);
            await _context.SaveChangesAsync();

            var os = await _context.OS
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            os.StatusId = vm.StatusId;
            _context.Update(os);
            //Registrar no Log de Auditoria

            var activity = new AuditTrail
            {
                Action = "Mudança de Status",
                TimeStamp = DateTime.Now,
                IpAdress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserId = userId,
                Module = "OS",
                AffectedTable = "OS"
            };
            _context.Add(activity);
            var client = await _context.Users.FirstOrDefaultAsync(u => u.Id == resolution.CreatedById);
            var status = await _context.systemCodeDetails.FindAsync(resolution.StatusId);
            await _emailTicketService.EnviarEmailAsync(
                    para: client.Email,
                    assunto: $"A Ordem de serviço nº {resolution.OSId} recebeu atualizações",
                    mensagem: $"Sua Ordem de serviço \"{resolution.OSId}\" foi atualizado por um técnico com sucesso e está com status \"{status?.Description}\".\n\n\"{resolution.Description}\"");

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: Tickets/Details/5

        public async Task<IActionResult> Details(int id, OSViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.OSDetails = await _context.OS
            .Include(t => t.CreatedBy)
            .Include(t => t.SubCategory)
            .Include(t => t.Status)
            .Include(t => t.Priority)
            .FirstOrDefaultAsync(m => m.Id == id);

            vm.OSComments = await _context.OSComment
                .Include(t => t.CreatedBy)
                .Include(t => t.OS)
                .Where(t => t.OSId == id)
                .ToListAsync();
            if (vm.OSDetails == null)
            {
                return NotFound();
            }

            return View(vm);
        }
        //GET: Ticket Resolve
        public async Task<IActionResult> Resolve(int id, OSViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.OSDetails = await _context.OS
            .Include(t => t.CreatedBy)
            .Include(t => t.SubCategory)
            .Include(t => t.Status)
            .Include(t => t.Priority)
            .FirstOrDefaultAsync(m => m.Id == id);

            vm.OSResolutions = await _context.OSResolutions
            .Include(t => t.CreatedBy)
            .Include(t => t.OS)
            .Include(t => t.Status)
            .Where(t => t.OSId == id)
            .ToListAsync();

            vm.OSComments = await _context.OSComment
                .Include(t => t.CreatedBy)
                .Include(t => t.OS)
                .Where(t => t.OSId == id)
                .ToListAsync();
            if (vm.OSDetails == null)
            {
                return NotFound();
            }
            ViewData["StatusId"] = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "STS"), "Id", "Description");
            return View(vm);
        }

        //GET: Ticket Assignment
        [HttpGet]
        public async Task<IActionResult> OSAssignment(int id, OSViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            vm.OSDetails = await _context.OS
            .Include(t => t.CreatedBy)
            .Include(t => t.SubCategory)
            .Include(t => t.Status)
            .Include(t => t.Priority)
            .FirstOrDefaultAsync(m => m.Id == id);

            vm.OSResolutions = await _context.OSResolutions
            .Include(t => t.CreatedBy)
            .Include(t => t.OS)
            .Include(t => t.Status)
            .Where(t => t.OSId == id)
            .ToListAsync();

            vm.OSComments = await _context.OSComment
                .Include(t => t.CreatedBy)
                .Include(t => t.OS)
                .Where(t => t.OSId == id)
                .ToListAsync();
            if (vm.OSDetails == null)
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

            var assignstatus = await _context.systemCodeDetails
                .Include(x => x.SystemCode)
                .Where(x => x.SystemCode.Code == "STS" && x.Code == "ATB")
                .FirstOrDefaultAsync();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            OSResolution resolution = new();
            resolution.OSId = id;
            resolution.CreatedById = userId;
            resolution.CreatedOn = DateTime.Now;
            resolution.StatusId = assignstatus.Id;
            resolution.Description = "Chamado Atribuído a um Técnico";
            _context.Add(resolution);
            await _context.SaveChangesAsync();

            var os = await _context.Tickets
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            os.StatusId = assignstatus.Id;
            os.AssignedToId = vm.AssignedToId;
            os.AssignedOn = DateTime.Now;
            _context.Update(os);
            //Registrar no Log de Auditoria

            var activity = new AuditTrail
            {
                Action = "Atribuição",
                TimeStamp = DateTime.Now,
                IpAdress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserId = userId,
                Module = "OS",
                AffectedTable = "OS"
            };
            _context.Add(activity);
            var client = await _context.Users.FirstOrDefaultAsync(u => u.Id == resolution.CreatedById);
            var status = await _context.systemCodeDetails.FindAsync(resolution.StatusId);
            await _emailTicketService.EnviarEmailAsync(
                    para: client.Email,
                    assunto: $"O Chamado nº {resolution.OSId} foi atribuído a um técnico",
                    mensagem: $"Seu chamado \"{resolution.OSId}\"  foi atribuído a um técnico com sucesso, por favor aguarde retorno.");

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: Tickets/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["PriorityId"] = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "PRD"), "Id", "Description");
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "FullName");
            ViewData["CategoryId"] = new SelectList(_context.OSCategories, "Id", "Name");
            ViewBag.Categorias = new SelectList(_context.OSCategories, "Id", "Name");
            ViewBag.Prioridades = new SelectList(_context.systemCodeDetails
                .Include(x => x.SystemCode)
                .Where(x => x.SystemCode.Code == "Priority"), "Id", "Description");
            return View();
        }

        // POST: Tickets/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OSViewModel vm, IFormFile anexo)
        {
            if (anexo?.Length > 0)
            {
                var filename = "OSAttachment" + DateTime.Now.ToString("yyyymmddhhmmss");
                var path = _configuration["FileSettings:UploadsFolder"]!;
                var filepath = Path.Combine(path, filename);
                var stream = new FileStream(filepath, FileMode.Create);
                await anexo.CopyToAsync(stream);
                vm.Attachment = filename;
            }
            var pending = await _context.systemCodeDetails
                .Include(x => x.SystemCode)
                .Where(x => x.SystemCode.Code == "STS" && x.Code == "PND")
                .FirstOrDefaultAsync();

            OS os = new();
            var categoria = await _context.TicketCategories.FindAsync(vm.CategoryId);
            if (categoria != null && categoria.DefaultPriorityId.HasValue)
            {
                os.PriorityId = categoria.DefaultPriorityId.Value;
            }
            os.CategoryId = os.CategoryId;


            os.Id = vm.Id;
            os.Title = vm.Title;
            os.Description = vm.Description;
            os.StatusId = pending.Id;
            os.SubCategoryId = vm.SubCategoryId;
            os.Attachment = vm.Attachment;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            os.CreatedOn = DateTime.Now;
            os.CreatedById = userId;
            _context.Add(os);
            await _context.SaveChangesAsync();

            //Registrar no Log de Auditoria

            var activity = new AuditTrail
            {
                Action = "Criar",
                TimeStamp = DateTime.Now,
                IpAdress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserId = userId,
                Module = "OS",
                AffectedTable = "OS"
            };
            _context.Add(activity);
            await _emailTicketService.EnviarEmailAsync(
                    para: User.FindFirstValue(ClaimTypes.Email),
                    assunto: $"Ordem de Serviço nº {os.Id} criado",
                    mensagem: $"Sua ordem de serviço \"{os.Title}\" foi criado com sucesso e está com status Pendente."
);

            await _context.SaveChangesAsync();
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "FullName", os.CreatedBy);
            ViewData["CategoryId"] = new SelectList(_context.TicketCategories, "Id", "Name");
            ViewData["PriorityId"] = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "PRD"), "Id", "Description");
            return RedirectToAction(nameof(Index));
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var os = await _context.OS
                .Include(t => t.Category)
                .Include(t => t.SubCategory)
                .Include(t => t.Priority)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (os == null)
                return NotFound();

            ViewBag.CategoryId = new SelectList(_context.OSCategories, "Id", "Name", os.CategoryId);
            ViewBag.SubCategoryId = new SelectList(_context.OSSubCategories.Where(x => x.CategoryId == os.CategoryId), "Id", "Name", os.SubCategoryId);
            ViewBag.PriorityId = new SelectList(_context.systemCodeDetails.Include(x => x.SystemCode).Where(x => x.SystemCode.Code == "PRD"), "Id", "Description", os.PriorityId);



            return View(os);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OS os)
        {
            if (id != os.Id)
                return NotFound();

            var OSOriginal = await _context.OS.FindAsync(id);

            if (OSOriginal == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            // Preserva os dados originais de criação
            _context.Entry(OSOriginal).Property(x => x.CreatedById).IsModified = false;
            _context.Entry(OSOriginal).Property(x => x.CreatedOn).IsModified = false;
            _context.Entry(OSOriginal).Property(x => x.StatusId).IsModified = false;

            // Marca como modificado
            OSOriginal.ModifiedOn = DateTime.Now;
            OSOriginal.ModifiedById = userId;
            OSOriginal.Id = os.Id;
            OSOriginal.Title = os.Title;
            OSOriginal.Description = os.Description;
            OSOriginal.CategoryId = os.CategoryId;
            OSOriginal.SubCategoryId = os.SubCategoryId;
            OSOriginal.Attachment = os.Attachment;



            _context.Update(OSOriginal);
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

            var os = await _context.OS
                .Include(t => t.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (os == null)
            {
                return NotFound();
            }

            return View(os);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var os = await _context.OS.FindAsync(id);
            if (os != null)
            {
                _context.OS.Remove(os);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OSExists(int id)
        {
            return _context.OS.Any(e => e.Id == id);
        }
    }
}
