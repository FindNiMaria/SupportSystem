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

namespace HelpdeskSystem.Controllers
{
    public class TicketSubCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketSubCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TicketSubCategories
        public async Task<IActionResult> Index(int id, TicketSubCategoriesVM vm)
        {
            vm.TicketSubCategories = await _context.TicketSubCategories
                .Include(t => t.Categoria)
                .Include(t => t.CriadoPor)
                .Include(t => t.ModificadoPor)
                .Where(x=>x.CategoriaId == id)
                .ToListAsync();

            vm.CategoriaId = id;
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
                .Include(t => t.CriadoPor)
                .Include(t => t.ModificadoPor)
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
            category.CategoriaId = Id;
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
            ticketSubCategory.CriadoPorId = usuariologado;
            ticketSubCategory.CriadoEm = DateTime.Now;

            ticketSubCategory.Id = 0;
            ticketSubCategory.CategoriaId = id;
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
            return RedirectToAction("Index", new {id=id});
            return View(ticketSubCategory);
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
            ViewData["CriadoPorId"] = new SelectList(_context.Users, "Id", "Id", ticketSubCategory.CriadoPorId);
            ViewData["ModificadoPorId"] = new SelectList(_context.Users, "Id", "Id", ticketSubCategory.ModificadoPorId);
            return View(ticketSubCategory);
        }

        // POST: TicketSubCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,TicketSubCategory ticketSubCategory)
        {
            if (id != ticketSubCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var usuariologado = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    ticketSubCategory.ModificadoPorId = usuariologado;
                    ticketSubCategory.ModificadoEm = DateTime.Now;
                    _context.Update(ticketSubCategory);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketSubCategoryExists(ticketSubCategory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ticketSubCategory);
        }

        // GET: TicketSubCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketSubCategory = await _context.TicketSubCategories
                .Include(t => t.CriadoPor)
                .Include(t => t.ModificadoPor)
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
            return RedirectToAction(nameof(Index));
        }

        private bool TicketSubCategoryExists(int id)
        {
            return _context.TicketSubCategories.Any(e => e.Id == id);
        }
    }
}
