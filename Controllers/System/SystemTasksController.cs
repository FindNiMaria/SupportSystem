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
    public class SystemTasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SystemTasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SystemTasks
        public async Task<IActionResult> Index(SystemTaskViewModel vm)
        {
            var query = _context.SystemTasks
                .Include(t => t.CreatedBy)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(vm.Code))
                query = query.Where(t => t.Code.Contains(vm.Code));

            if (!string.IsNullOrWhiteSpace(vm.Name))
                query = query.Where(t => t.Name.Contains(vm.Name));

            if (!string.IsNullOrWhiteSpace(vm.CreatedById))
                query = query.Where(t => t.CreatedById == vm.CreatedById);

            if (vm.CreatedFrom.HasValue)
                query = query.Where(t => t.CreatedOn >= vm.CreatedFrom.Value);

            if (vm.CreatedTo.HasValue)
                query = query.Where(t => t.CreatedOn <= vm.CreatedTo.Value);

            vm.Tasks = await query.OrderByDescending(t => t.CreatedOn).ToListAsync();
            vm.Users = new SelectList(_context.Users, "Id", "FullName");

            return View(vm);
        }


        // GET: SystemTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemTask = await _context.SystemTasks
                .Include(s => s.Parent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemTask == null)
            {
                return NotFound();
            }

            return View(systemTask);
        }

        // GET: SystemTasks/Create
        public IActionResult Create()
        {
            ViewData["ParentId"] = new SelectList(_context.SystemTasks, "Id", "Name");
            return View();
        }

        // POST: SystemTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SystemTask systemTask)
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            systemTask.CreatedById = userid;
            systemTask.CreatedOn = DateTime.Now;
                _context.Add(systemTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            ViewData["ParentId"] = new SelectList(_context.SystemTasks, "Id", "Name", systemTask.ParentId);
        }

        // GET: SystemTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemTask = await _context.SystemTasks.FindAsync(id);
            if (systemTask == null)
            {
                return NotFound();
            }
            ViewData["ParentId"] = new SelectList(_context.SystemTasks, "Id", "Name", systemTask.ParentId);
            return View(systemTask);
        }

        // POST: SystemTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SystemTask systemTask)
        {
            if (id != systemTask.Id)
            {
                return NotFound();
            }
            var existingTask = await _context.SystemTasks.FindAsync(id);
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Entry(existingTask).Property(x => x.CreatedById).IsModified = false;
            _context.Entry(existingTask).Property(x => x.CreatedOn).IsModified = false;
            existingTask.ParentId = systemTask.ParentId;
            existingTask.Code = systemTask.Code;
            existingTask.Name = systemTask.Name;
            existingTask.OrderNo = systemTask.OrderNo;
            existingTask.ModifiedById = userid;
            existingTask.ModifiedOn = DateTime.Now;
            _context.Update(existingTask);
            await _context.SaveChangesAsync();

                    
            ViewData["ParentId"] = new SelectList(_context.SystemTasks, "Id", "Name", systemTask.ParentId);
            return RedirectToAction(nameof(Index));
        }

        // GET: SystemTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemTask = await _context.SystemTasks
                .Include(s => s.Parent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemTask == null)
            {
                return NotFound();
            }

            return View(systemTask);
        }

        // POST: SystemTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var systemTask = await _context.SystemTasks.FindAsync(id);
            if (systemTask != null)
            {
                _context.SystemTasks.Remove(systemTask);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemTaskExists(int id)
        {
            return _context.SystemTasks.Any(e => e.Id == id);
        }
    }
}
