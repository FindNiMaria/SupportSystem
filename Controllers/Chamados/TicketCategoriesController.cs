using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpdeskSystem.Data;
using HelpdeskSystem.Models;
using System.Security.Claims;
using HelpdeskSystem.Models.Ticket;
using HelpdeskSystem.Models.User;
using HelpdeskSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace HelpdeskSystem.Controllers.Chamados
{
    [Authorize(Roles = "Administrador")]
    public class TicketCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TicketCategories
        public async Task<IActionResult> Index(TicketCategoryViewModel vm)
        {
            var query = _context.TicketCategories
                .Include(t => t.CreatedBy)
                .Include(t => t.ModifiedBy)
                .Include(t => t.DefautPriority)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(vm.Code))
                query = query.Where(t => t.Code.Contains(vm.Code));

            if (!string.IsNullOrWhiteSpace(vm.Name))
                query = query.Where(t => t.Name.Contains(vm.Name));

            if (vm.DefaultPriorityId.HasValue)
                query = query.Where(t => t.DefaultPriorityId == vm.DefaultPriorityId);

            vm.Categories = await query.OrderBy(t => t.Name).ToListAsync();

            vm.Priorities = new SelectList(
                _context.systemCodeDetails
                    .Include(x => x.SystemCode)
                    .Where(x => x.SystemCode.Code == "PRD"),
                "Id",
                "Description"
            );

            return View(vm);
        }


        // GET: TicketCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var ticketCategory = await _context.TicketCategories
                .Include(t => t.CreatedBy)
                .Include(t => t.ModifiedBy)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ticketCategory == null) return NotFound();

            return View(ticketCategory);
        }

        // GET: TicketCategories/Create
        public IActionResult Create()
        {
            ViewBag.Prioridades = new SelectList(
                _context.systemCodeDetails
                    .Include(x => x.SystemCode)
                    .Where(x => x.SystemCode.Code == "PRD"),"Id","Description"
            );

            return View();
        }

        // POST: TicketCategories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketCategory ticketCategory)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ticketCategory.CreatedOn = DateTime.Now;
            ticketCategory.CreatedById = userId;

            _context.Add(ticketCategory);
            await _context.SaveChangesAsync();

            var activity = new AuditTrail
            {
                Action = "Criar",
                TimeStamp = DateTime.Now,
                IpAdress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserId = userId,
                Module = "Categoria de Chamado",
                AffectedTable = "Categoria de Chamados"
            };

            _context.Add(activity);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: TicketCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var ticketCategory = await _context.TicketCategories.FindAsync(id);
            if (ticketCategory == null) return NotFound();

            ViewBag.Priority = new SelectList(
                _context.systemCodeDetails
                    .Include(x => x.SystemCode)
                    .Where(x => x.SystemCode.Code == "PRD"),
                "Id",
                "Description",
                ticketCategory.DefaultPriorityId
            );

            return View(ticketCategory);
        }

        // POST: TicketCategories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TicketCategory ticketCategory)
        {
            if (id != ticketCategory.Id) return NotFound();

            var category = await _context.TicketCategories.FindAsync(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            category.ModifiedOn = DateTime.Now;
            category.ModifiedById = userId;
            category.Code = ticketCategory.Code;
            category.Name = ticketCategory.Name;
            _context.Entry(category).Property(x => x.CreatedById).IsModified = false;
            _context.Entry(category).Property(x => x.CreatedOn).IsModified = false;

            _context.Update(category);
            await _context.SaveChangesAsync();

            var activity = new AuditTrail
            {
                Action = "Editar",
                TimeStamp = DateTime.Now,
                IpAdress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserId = userId,
                Module = "Categoria de Chamado",
                AffectedTable = "Categoria de Chamados"
            };

            _context.Add(activity);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: TicketCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var ticketCategory = await _context.TicketCategories
                .Include(t => t.CreatedBy)
                .Include(t => t.ModifiedBy)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ticketCategory == null) return NotFound();

            return View(ticketCategory);
        }

        // POST: TicketCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticketCategory = await _context.TicketCategories.FindAsync(id);
            if (ticketCategory != null)
            {
                _context.TicketCategories.Remove(ticketCategory);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TicketCategoryExists(int id)
        {
            return _context.TicketCategories.Any(e => e.Id == id);
        }
    }
}
