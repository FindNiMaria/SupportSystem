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
using HelpdeskSystem.ViewModels;
using HelpdeskSystem.Models.Ticket;
using HelpdeskSystem.Models.User;
using Microsoft.AspNetCore.Authorization;

namespace HelpdeskSystem.Controllers.Chamados
{
    [Authorize(Roles = "Administrador")]
    public class TicketSubCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketSubCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TicketSubCategories
        public async Task<IActionResult> Index(int id, TicketSubCategoryViewModel vm)
        {
            var query = _context.TicketSubCategories
                .Include(t => t.Category)
                .Include(t => t.CreatedBy)
                .Include(t => t.ModifiedBy)
                .Where(x => x.CategoryId == id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(vm.Code))
                query = query.Where(x => x.Code.Contains(vm.Code));

            if (!string.IsNullOrWhiteSpace(vm.Name))
                query = query.Where(x => x.Name.Contains(vm.Name));

            vm.TicketSubCategories = await query.ToListAsync();
            vm.CategoryId = id;
            vm.Categories = new SelectList(_context.TicketCategories, "Id", "Name");

            return View(vm);
        }


        // GET: TicketSubCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketSubCategory = await _context.TicketSubCategories
                .Include(t => t.CreatedBy)
                .Include(t => t.ModifiedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketSubCategory == null)
            {
                return NotFound();
            }

            return View(ticketSubCategory);
        }

        // GET: TicketSubCategories/Create
        public IActionResult Create(int Id)
        {
            TicketSubCategory category = new();
            category.CategoryId = Id;
            return View(category);
        }

        // POST: TicketSubCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id,TicketSubCategory ticketSubCategory)
        {
            var usuariologado = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ticketSubCategory.CreatedById = usuariologado;
            ticketSubCategory.CreatedOn = DateTime.Now;

            ticketSubCategory.Id = 0;
            ticketSubCategory.CategoryId = id;
            _context.Add(ticketSubCategory);

            //Registrar no Log de Auditoria

            var activity = new AuditTrail
            {
                Action = "Criar",
                TimeStamp = DateTime.Now,
                IpAdress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserId = usuariologado,
                Module = "Sub-Categorias",
                AffectedTable = "Sub-Categoria de Chamados"
            };
            _context.Add(activity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new {id});
        }

        // GET: TicketSubCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketSubCategory = await _context.TicketSubCategories.FindAsync(id);
            if (ticketSubCategory == null)
            {
                return NotFound();
            }
        
            return View(ticketSubCategory);
        }

        // POST: TicketSubCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TicketSubCategory input)
        {
            if (id != input.Id)
                return NotFound();

            // Busca original do banco
            var original = await _context.TicketSubCategories
                .Include(x => x.Category)
                .Include(x => x.CreatedBy)
                .Include(x => x.ModifiedBy)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (original == null)
                return NotFound();

            var usuariologado = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Atualiza SOMENTE os campos que o usuário pôde editar
            original.Name = input.Name;
            original.Code = input.Code;

            // Atualiza os metadados
            original.ModifiedById = usuariologado;
            original.ModifiedOn = DateTime.Now;

            try
            {
                _context.Update(original);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", new { id = original.CategoryId });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TicketSubCategories.Any(x => x.Id == input.Id))
                    return NotFound();
                else
                    throw;
            }
        }





        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketSubCategory = await _context.TicketSubCategories
                .Include(t => t.Category)          // entidade relacionada (exemplo)
                .Include(t => t.CreatedBy)         // usuário que criou
                .Include(t => t.ModifiedBy)        // usuário que modificou
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ticketSubCategory == null)
            {
                return NotFound();
            }

            return View(ticketSubCategory);
        }


        // POST: TicketSubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticketSubCategory = await _context.TicketSubCategories.FindAsync(id);
            if (ticketSubCategory != null)
            {
                _context.TicketSubCategories.Remove(ticketSubCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { id = ticketSubCategory.CategoryId });
        }

        private bool TicketSubCategoryExists(int id)
        {
            return _context.TicketSubCategories.Any(e => e.Id == id);
        }
    }
}
