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
                .Include(x=>x.CreatedBy)
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
            systemCode.CreatedOn = DateTime.Now;
            systemCode.CreatedById = userId;
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
            // Verifique os valores de id e systemCodeDetail.Id para garantir que eles são iguais
            Console.WriteLine($"id: {id}, systemCodeDetail.Id: {systemCode.Id}");

            if (id != systemCode.Id)
            {
                return NotFound();
            }
            var existing = await _context.systemCodeDetails.FindAsync(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            existing.Code = systemCode.Code; 
            existing.Description = systemCode.Description; 
            existing.ModifiedOn = DateTime.Now;
            existing.ModifiedById = userId;
            _context.Entry(existing).Property(x => x.CreatedById).IsModified = false;
            _context.Entry(existing).Property(x => x.CreatedOn).IsModified = false;

            _context.Update(existing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
