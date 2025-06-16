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
using HelpdeskSystem.Models.SO;
using HelpdeskSystem.Models.User;


namespace HelpdeskSystem.Controllers.SO
{
    public class OSController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailOSService _emailOSService;


        public OSController(ApplicationDbContext context, IConfiguration configuration, IEmailOSService emailOSService)
        {

            _context = context;
            _configuration = configuration;
            _emailOSService = emailOSService;

        }

        public async Task<IActionResult> ImportEmails()
        {
            await _emailOSService.ImportarEmailsComoOSAsync();
            return RedirectToAction("Index");
        }
        // GET: Tickets
        public async Task<IActionResult> Index(OSViewModel vm)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Users
                .Include(u => u.Department)
                .FirstOrDefaultAsync(u => u.Id == userId);

            // Consulta base com includes
            var query = _context.OS
                .Include(t => t.AssignedTo)
                .Include(t => t.Status)
                .Include(t => t.Priority)
                .Include(t => t.Category)
                .Include(t => t.SubCategory)
                .Include(t => t.CreatedBy)
                .Include(t => t.OSComments)
                .AsQueryable();

            //  Regras de visibilidade baseadas em permissões
            if (PermissionHelper.IsAdmin(User))
            {
                // Admin vê todos os chamados
            }
            else if (PermissionHelper.IsTecnico(User))
            {
                query = query.Where(t => t.AssignedToId == userId);

            }
            else if (PermissionHelper.IsCoordenador(User))
            {
                query = query.Where(t => t.CreatedBy.DepartmentId == user.DepartmentId);
            }
            else
            {
                query = query.Where(t => t.CreatedById == userId);
            }

            // Aplicar filtros
            if (vm.CategoryId > 0)
                query = query.Where(t => t.CategoryId == vm.CategoryId);

            if (vm.PriorityId > 0)
                query = query.Where(t => t.PriorityId == vm.PriorityId);

            if (vm.StatusId > 0)
                query = query.Where(t => t.StatusId == vm.StatusId);

            if (!string.IsNullOrEmpty(vm.TechnicianId))
                query = query.Where(t => t.AssignedToId == vm.TechnicianId);

            // Obter e ordenar os chamados
            var os = await query
                .OrderByDescending(t => t.CreatedOn)
                .ToListAsync();

            // Preencher ViewModel
            vm.OS = os.Select(t => new OSViewModel
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                StatusId = t.StatusId,
                Status = t.Status,
                PriorityId = t.PriorityId,
                Priority = t.Priority,
                CreatedById = t.CreatedById,
                CreatedBy = t.CreatedBy,
                CreatedOn = t.CreatedOn,
                CategoryId = t.CategoryId,
                SubCategoryId = t.SubCategoryId ?? 0,
                SubCategory = t.SubCategory,
                Attachment = t.Attachment,
                AssignedToId = t.AssignedToId,
                AssignedTo = t.AssignedTo,
                AssignedOn = t.AssignedOn,
                OSComments = t.OSComments.ToList()
            }).ToList();

            // SelectLists para filtros na view
            vm.Categories = new SelectList(_context.OSCategories, "Id", "Name");
            vm.Priorities = new SelectList(_context.systemCodeDetails.Where(x => x.SystemCode.Code == "PRD"), "Id", "Description");
            vm.Statuses = new SelectList(_context.systemCodeDetails.Where(x => x.SystemCode.Code == "STS"), "Id", "Description");
            vm.Technicians = new SelectList(_context.Users, "Id", "FullName");

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
        //POST: Add status resolved
        [HttpPost]
        public async Task<IActionResult> ResolvedConfirmed(int id, OSViewModel vm)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            OSResolution resolution = new();
            resolution.OSId = id;
            resolution.CreatedById = userId;
            resolution.CreatedOn = DateTime.Now;
            resolution.Description = vm.OSDescription ?? "Sem Descrição";
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
            await _emailOSService.EnviarEmailAsync(
                    para: client.Email,
                    assunto: $"A OS nº {resolution.OSId} recebeu atualizações",
                    mensagem: $"Sua OS \"{resolution.OSId}\" foi atualizada por um técnico com sucesso e está com status \"{status?.Description}\".\n\n\"{resolution.Description}\"");

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
        //GET: OS Resolve
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
        // GET: Carrega dados para tela de atribuição
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
            ViewBag.Technicians = new SelectList(
             await _context.Users
            .Where(u => u.Role == "Técnico")
            .ToListAsync(),
            "Id", "FullName");
            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignmentConfirmed(OSViewModel vm)
        {


            var assignstatus = await _context.systemCodeDetails
                   .Include(x => x.SystemCode)
                   .Where(x => x.SystemCode.Code == "STS" && x.Code == "ATB")
                   .FirstOrDefaultAsync();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            OSResolution resolution = new();
            resolution.OSId = vm.Id;
            resolution.CreatedById = userId;
            resolution.CreatedOn = DateTime.Now;
            resolution.StatusId = assignstatus.Id;
            resolution.Description = "OS Atribuído a um Técnico";
            _context.Add(resolution);
            await _context.SaveChangesAsync();

            var os = await _context.OS
                .Where(x => x.Id == vm.Id)
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
            await _emailOSService.EnviarEmailAsync(
                    para: client.Email,
                    assunto: $"Sua OS nº {resolution.OSId} foi atribuída a um técnico",
                    mensagem: $"Sua OS \"{resolution.OSId}\"  foi atribuída a um técnico com sucesso, por favor aguarde retorno.");



            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }





        // GET: OS/Create
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

        // POST: 

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
            os.CategoryId = vm.CategoryId;


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
            await _emailOSService.EnviarEmailAsync(
                    para: User.FindFirstValue(ClaimTypes.Email),
                    assunto: $"Chamado nº {os.Id} criado",
                    mensagem: $"Seu chamado \"{os.Title}\" foi criado com sucesso e está com status Pendente."
);

            await _context.SaveChangesAsync();
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "FullName", os.CreatedBy);
            ViewData["CategoryId"] = new SelectList(_context.OSCategories, "Id", "Name");
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

            var Original = await _context.OS.FindAsync(id);

            if (Original == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            // Preserva os dados originais de criação
            _context.Entry(Original).Property(x => x.CreatedById).IsModified = false;
            _context.Entry(Original).Property(x => x.CreatedOn).IsModified = false;
            _context.Entry(Original).Property(x => x.StatusId).IsModified = false;

            // Marca como modificado
            Original.ModifiedOn = DateTime.Now;
            Original.ModifiedById = userId;
            Original.Id = os.Id;
            Original.Title = os.Title;
            Original.Description = os.Description;
            Original.CategoryId = os.CategoryId;
            Original.SubCategoryId = os.SubCategoryId;
            Original.Attachment = os.Attachment;



            _context.Update(Original);
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
            var os = await _context.Tickets.FindAsync(id);
            if (os != null)
            {
                _context.Tickets.Remove(os);
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
