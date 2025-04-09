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

namespace HelpdeskSystem.Controllers
{
    public class SystemCodesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SystemCodesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SystemCodes
        public async Task<IActionResult> Index()
        {
            var systemCodes = await _context
                .systemCodes
                .Include(x=>x.CriadoPor)
                .ToListAsync();


            return View(systemCodes);
        }

        // GET: SystemCodes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemCode = await _context.systemCodes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemCode == null)
            {
                return NotFound();
            }

            return View(systemCode);
        }

        // GET: SystemCodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SystemCodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SystemCode systemCode)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            systemCode.CriadoEm = DateTime.Now;
            systemCode.CriadoPorId = userId;
            _context.Add(systemCode);
            await _context.SaveChangesAsync();
            var activity = new AuditTrail
            {
                Action = "Criar",
                TimeStamp = DateTime.Now,
                IpAdress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserId = userId,
                Module = "SystemCodes",
                AffectedTable = "SystemCodes"
            };

            return RedirectToAction(nameof(Index));
            return View(systemCode);
        }
      

        // GET: SystemCodes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemCode = await _context.systemCodes.FindAsync(id);
            if (systemCode == null)
            {
                return NotFound();
            }
            return View(systemCode);
        }

        // POST: SystemCodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SystemCode systemCode)
        {

            if (id != systemCode.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    systemCode.ModificadoEm = DateTime.Now;
                    systemCode.ModificadoPorId = userId;
                    _context.Update(systemCode);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemCodeExists(systemCode.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            return View(systemCode);
        }

        // GET: SystemCodes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemCode = await _context.systemCodes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemCode == null)
            {
                return NotFound();
            }

            return View(systemCode);
        }

        // POST: SystemCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var systemCode = await _context.systemCodes.FindAsync(id);
            if (systemCode != null)
            {
                _context.systemCodes.Remove(systemCode);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemCodeExists(int id)
        {
            return _context.systemCodes.Any(e => e.Id == id);
        }
    }
}
