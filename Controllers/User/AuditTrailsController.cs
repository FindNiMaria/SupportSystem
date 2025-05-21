using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpdeskSystem.Data;
using HelpdeskSystem.Models.User;
using HelpdeskSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace HelpdeskSystem.Controllers.User
{
    [Authorize(Roles = "Administrador")]
    public class AuditTrailsController : Controller
    {
        
        private readonly ApplicationDbContext _context;

        public AuditTrailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AuditTrails
        public async Task<IActionResult> Index(AuditTrailViewModel vm)
        {
            
            var query = _context.AuditTrails
                .Include(a => a.User)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(vm.Module))
                query = query.Where(a => a.Module.Contains(vm.Module));

            if (!string.IsNullOrWhiteSpace(vm.Action))
                query = query.Where(a => a.Action.Contains(vm.Action));

            if (!string.IsNullOrWhiteSpace(vm.UserId))
                query = query.Where(a => a.UserId == vm.UserId);

            if (!string.IsNullOrWhiteSpace(vm.AffectedTable))
                query = query.Where(a => a.AffectedTable == vm.AffectedTable);

            if (vm.From.HasValue)
                query = query.Where(a => a.TimeStamp >= vm.From.Value);

            if (vm.To.HasValue)
                query = query.Where(a => a.TimeStamp <= vm.To.Value);

            vm.Logs = await query.OrderByDescending(a => a.TimeStamp).ToListAsync();
            vm.Users = new SelectList(_context.Users, "Id", "FullName");

            return View(vm);
        }


        // GET: AuditTrails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auditTrail = await _context.AuditTrails
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auditTrail == null)
            {
                return NotFound();
            }

            return View(auditTrail);
        }

        // GET: AuditTrails/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: AuditTrails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Action,Module,AffectedTable,TimeStamp,IpAdress,UserId")] AuditTrail auditTrail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(auditTrail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", auditTrail.UserId);
            return View(auditTrail);
        }

        // GET: AuditTrails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auditTrail = await _context.AuditTrails.FindAsync(id);
            if (auditTrail == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", auditTrail.UserId);
            return View(auditTrail);
        }

        // POST: AuditTrails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Action,Module,AffectedTable,TimeStamp,IpAdress,UserId")] AuditTrail auditTrail)
        {
            if (id != auditTrail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(auditTrail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuditTrailExists(auditTrail.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", auditTrail.UserId);
            return View(auditTrail);
        }

        // GET: AuditTrails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auditTrail = await _context.AuditTrails
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auditTrail == null)
            {
                return NotFound();
            }

            return View(auditTrail);
        }

        // POST: AuditTrails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auditTrail = await _context.AuditTrails.FindAsync(id);
            if (auditTrail != null)
            {
                _context.AuditTrails.Remove(auditTrail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuditTrailExists(int id)
        {
            return _context.AuditTrails.Any(e => e.Id == id);
        }
    }
}
