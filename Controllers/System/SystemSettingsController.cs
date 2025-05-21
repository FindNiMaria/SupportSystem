using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpdeskSystem.Data;
using System.Security.Claims;
using HelpdeskSystem.Models.System;
using HelpdeskSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace HelpdeskSystem.Controllers.System
{
    [Authorize(Roles = "Administrador")]
    public class SystemSettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SystemSettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SystemSettings
        public async Task<IActionResult> Index(SystemSettingsViewModel vm)
        {
            var query = _context.SystemSettings.AsQueryable();

            if (!string.IsNullOrWhiteSpace(vm.Code))
                query = query.Where(s => s.Code.Contains(vm.Code));

            if (!string.IsNullOrWhiteSpace(vm.Name))
                query = query.Where(s => s.Name.Contains(vm.Name));

            vm.Settings = await query.OrderBy(s => s.Code).ToListAsync();

            return View(vm);
        }


        // GET: SystemSettings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemSettings = await _context.SystemSettings
                .Include(s => s.CreatedBy)
                .Include(s => s.ModifiedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemSettings == null)
            {
                return NotFound();
            }

            return View(systemSettings);
        }

        // GET: SystemSettings/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ModifiedById"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: SystemSettings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SystemSettings systemSettings)
        {


            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            systemSettings.CreatedById = userid;
            systemSettings.CreatedOn = DateTime.Now;
            _context.Add(systemSettings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

        }

        // GET: SystemSettings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemSettings = await _context.SystemSettings.FindAsync(id);
            if (systemSettings == null)
            {
                return NotFound();
            }
            
            return View(systemSettings);
        }

        // POST: SystemSettings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SystemSettings systemSettings)
        {
            if (id != systemSettings.Id)
            {
                return NotFound();
            }
            var existingSettings = await _context.SystemSettings.FindAsync(id);
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Entry(existingSettings).Property(x => x.CreatedById).IsModified = false;
            _context.Entry(existingSettings).Property(x => x.CreatedOn).IsModified = false;
            existingSettings.Code = systemSettings.Code;
            existingSettings.Name = systemSettings.Name;
            existingSettings.Value = systemSettings.Value;
            existingSettings.ModifiedById = userid;
            existingSettings.ModifiedOn = DateTime.Now;
            _context.Update(existingSettings);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
        // GET: SystemSettings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemSettings = await _context.SystemSettings
                .Include(s => s.CreatedBy)
                .Include(s => s.ModifiedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemSettings == null)
            {
                return NotFound();
            }

            return View(systemSettings);
        }

        // POST: SystemSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var systemSettings = await _context.SystemSettings.FindAsync(id);
            if (systemSettings != null)
            {
                _context.SystemSettings.Remove(systemSettings);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemSettingsExists(int id)
        {
            return _context.SystemSettings.Any(e => e.Id == id);
        }
    }
}
