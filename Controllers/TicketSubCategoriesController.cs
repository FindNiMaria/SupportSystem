using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpdeskSystem.Data;
using HelpdeskSystem.Models;
using HelpdeskSystem.Data.Migrations;
using System.Security.Claims;

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
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TicketSubCategories.Include(t => t.Category).Include(t => t.CriadoPor).Include(t => t.ModificadoPor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TicketSubCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketSubCategory = await _context.TicketSubCategories
                .Include(t => t.Category)
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
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.TicketCategories, "Id", "Id");
            ViewData["CriadoPorId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ModificadoPorId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: TicketSubCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryId,Codigo,Nome,CriadoPorId,CriadoEm,ModificadoPorId,ModificadoEm")] TicketSubCategory ticketSubCategory)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ticketSubCategory.ModificadoEm = DateTime.Now;
            ticketSubCategory.ModificadoPorId = userId;
            _context.Add(ticketSubCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            
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
            ViewData["CategoryId"] = new SelectList(_context.TicketCategories, "Id", "Id", ticketSubCategory.CategoryId);
            ViewData["CriadoPorId"] = new SelectList(_context.Users, "Id", "Id", ticketSubCategory.CriadoPorId);
            ViewData["ModificadoPorId"] = new SelectList(_context.Users, "Id", "Id", ticketSubCategory.ModificadoPorId);
            return View(ticketSubCategory);
        }

        // POST: TicketSubCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TicketSubCategory ticketSubCategory)
        {
            if (id != ticketSubCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    ticketSubCategory.ModificadoEm = DateTime.Now;
                    ticketSubCategory.ModificadoPorId = userId;
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
                .Include(t => t.Category)
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
