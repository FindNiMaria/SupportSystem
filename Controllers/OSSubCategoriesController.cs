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
    public class OSSubCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OSSubCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OSSubCategories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OSSubCategories.Include(o => o.Category).Include(o => o.CriadoPor).Include(o => o.ModificadoPor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OSSubCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oSSubCategory = await _context.OSSubCategories
                .Include(o => o.Category)
                .Include(o => o.CriadoPor)
                .Include(o => o.ModificadoPor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oSSubCategory == null)
            {
                return NotFound();
            }

            return View(oSSubCategory);
        }

        // GET: OSSubCategories/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.OSCategories, "Id", "Id");
            ViewData["CriadoPorId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ModificadoPorId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: OSSubCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( OSSubCategory oSSubCategory)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            oSSubCategory.CriadoEm = DateTime.Now;
            oSSubCategory.CriadoPorId = userId;
            _context.Add(oSSubCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                return View(oSSubCategory);
        }

        // GET: OSSubCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oSSubCategory = await _context.OSSubCategories.FindAsync(id);
            if (oSSubCategory == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.OSCategories, "Id", "Id", oSSubCategory.CategoryId);
            ViewData["CriadoPorId"] = new SelectList(_context.Users, "Id", "Id", oSSubCategory.CriadoPorId);
            ViewData["ModificadoPorId"] = new SelectList(_context.Users, "Id", "Id", oSSubCategory.ModificadoPorId);
            return View(oSSubCategory);
        }

        // POST: OSSubCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OSSubCategory oSSubCategory)
        {
            if (id != oSSubCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    oSSubCategory.ModificadoEm = DateTime.Now;
                    oSSubCategory.ModificadoPorId = userId;
                    _context.Update(oSSubCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OSSubCategoryExists(oSSubCategory.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.OSCategories, "Id", "Id", oSSubCategory.CategoryId);
            ViewData["CriadoPorId"] = new SelectList(_context.Users, "Id", "Id", oSSubCategory.CriadoPorId);
            ViewData["ModificadoPorId"] = new SelectList(_context.Users, "Id", "Id", oSSubCategory.ModificadoPorId);
            return View(oSSubCategory);
        }

        // GET: OSSubCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oSSubCategory = await _context.OSSubCategories
                .Include(o => o.Category)
                .Include(o => o.CriadoPor)
                .Include(o => o.ModificadoPor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oSSubCategory == null)
            {
                return NotFound();
            }

            return View(oSSubCategory);
        }

        // POST: OSSubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oSSubCategory = await _context.OSSubCategories.FindAsync(id);
            if (oSSubCategory != null)
            {
                _context.OSSubCategories.Remove(oSSubCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OSSubCategoryExists(int id)
        {
            return _context.OSSubCategories.Any(e => e.Id == id);
        }
    }
}
