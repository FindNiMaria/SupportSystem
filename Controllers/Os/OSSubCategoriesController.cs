using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpdeskSystem.Data;
using System.Security.Claims;
using HelpdeskSystem.ViewModels;
using HelpdeskSystem.Models.SO;
using HelpdeskSystem.Models.User;
using Microsoft.AspNetCore.Authorization;

namespace HelpdeskSystem.Controllers.SO
{
    [Authorize(Roles = "Administrador")]
    public class OSSubCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OSSubCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TicketSubCategories
        public async Task<IActionResult> Index(int id, OSSubCategoryViewModel vm)
        {
            var query = _context.OSSubCategories
                .Include(t => t.Category)
                .Include(t => t.CreatedBy)
                .Include(t => t.ModifiedBy)
                .Where(x => x.CategoryId == id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(vm.Code))
                query = query.Where(x => x.Code.Contains(vm.Code));

            if (!string.IsNullOrWhiteSpace(vm.Name))
                query = query.Where(x => x.Name.Contains(vm.Name));

            vm.OSSubCategories = await query.ToListAsync();
            vm.CategoryId = id;
            vm.Categories = new SelectList(_context.OSCategories, "Id", "Name");

            return View(vm);
        }


        // GET: TicketSubCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var osSubCategory = await _context.OSSubCategories
                .Include(t => t.CreatedBy)
                .Include(t => t.ModifiedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (osSubCategory == null)
            {
                return NotFound();
            }

            return View(osSubCategory);
        }

        // GET: TicketSubCategories/Create
        public IActionResult Create(int Id)
        {
            OSSubCategory category = new();
            category.CategoryId = Id;
            return View(category);
        }

        // POST: TicketSubCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, OSSubCategory osSubCategory)
        {
            var usuariologado = User.FindFirstValue(ClaimTypes.NameIdentifier);
            osSubCategory.CreatedById = usuariologado;
            osSubCategory.CreatedOn = DateTime.Now;

            osSubCategory.Id = 0;
            osSubCategory.CategoryId = id;
            _context.Add(osSubCategory);

            //Registrar no Log de Auditoria

            var activity = new AuditTrail
            {
                Action = "Criar",
                TimeStamp = DateTime.Now,
                IpAdress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserId = usuariologado,
                Module = "Sub-Categorias",
                AffectedTable = "Sub-Categoria de OS"
            };
            _context.Add(activity);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { id });
        }

        // GET: TicketSubCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var osSubCategory = await _context.OSSubCategories.FindAsync(id);
            if (osSubCategory == null)
            {
                return NotFound();
            }
            ViewData["CriadoPorId"] = new SelectList(_context.Users, "Id", "Id", osSubCategory.CreatedById);
            ViewData["ModificadoPorId"] = new SelectList(_context.Users, "Id", "Id", osSubCategory.ModifiedById);
            return View(osSubCategory);
        }

        // POST: TicketSubCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OSSubCategory osSubCategory)
        {
            if (id != osSubCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var usuariologado = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    osSubCategory.ModifiedById = usuariologado;
                    osSubCategory.ModifiedOn = DateTime.Now;
                    _context.Update(osSubCategory);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OSSubCategoryExists(osSubCategory.Id))
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
            return View(osSubCategory);
        }

        // GET: TicketSubCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var osSubCategory = await _context.OSSubCategories
                .Include(t => t.CreatedOn)
                .Include(t => t.ModifiedOn)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (osSubCategory == null)
            {
                return NotFound();
            }

            return View(osSubCategory);
        }

        // POST: TicketSubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var osSubCategory = await _context.OSSubCategories.FindAsync(id);
            if (osSubCategory != null)
            {
                _context.OSSubCategories.Remove(osSubCategory);
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
