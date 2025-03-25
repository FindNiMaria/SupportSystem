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
using HelpdeskSystem.Data.Migrations;

namespace HelpdeskSystem.Controllers
{
    public class OSCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OSCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OSCategories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OSCategories.Include(o => o.CriadoPor).Include(o => o.ModificadoPor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OSCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oSCategory = await _context.OSCategories
                .Include(o => o.CriadoPor)
                .Include(o => o.ModificadoPor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oSCategory == null)
            {
                return NotFound();
            }

            return View(oSCategory);
        }

        // GET: OSCategories/Create
        public IActionResult Create()
        {
            ViewData["CriadoPorId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ModificadoPorId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: OSCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.OSCategory oSCategory)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            oSCategory.CriadoEm = DateTime.Now;
            oSCategory.CriadoPorId = userId;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            return View(oSCategory);
        }

        // GET: OSCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oSCategory = await _context.OSCategories.FindAsync(id);
            if (oSCategory == null)
            {
                return NotFound();
            }
            ViewData["CriadoPorId"] = new SelectList(_context.Users, "Id", "Id", oSCategory.CriadoPorId);
            ViewData["ModificadoPorId"] = new SelectList(_context.Users, "Id", "Id", oSCategory.ModificadoPorId);
            return View(oSCategory);
        }

        // POST: OSCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Models.OSCategory oSCategory)
        {
            if (id != oSCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    oSCategory.ModificadoEm = DateTime.Now;
                    oSCategory.ModificadoPorId = userId;

                    _context.Update(oSCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OSCategoryExists(oSCategory.Id))
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
           
            return View(oSCategory);
        }

        // GET: OSCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oSCategory = await _context.OSCategories
                .Include(o => o.CriadoPor)
                .Include(o => o.ModificadoPor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oSCategory == null)
            {
                return NotFound();
            }

            return View(oSCategory);
        }

        // POST: OSCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oSCategory = await _context.OSCategories.FindAsync(id);
            if (oSCategory != null)
            {
                _context.OSCategories.Remove(oSCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OSCategoryExists(int id)
        {
            return _context.OSCategories.Any(e => e.Id == id);
        }
    }
}
