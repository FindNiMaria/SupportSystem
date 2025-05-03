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
    public class SystemCodeDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SystemCodeDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SystemCodeDetails
        public async Task<IActionResult> Index()
        {
            
            var systemCodeDetail = await _context
                .systemCodeDetails
                .Include(s => s.SystemCode)
                .Include(x => x.CreatedBy)
                .ToListAsync();

            return View(systemCodeDetail);
        }

        // GET: SystemCodeDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemCodeDetail = await _context.systemCodeDetails
                .Include(s => s.SystemCode)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemCodeDetail == null)
            {
                return NotFound();
            }

            return View(systemCodeDetail);
        }

        // GET: SystemCodeDetails/Create
        public IActionResult Create()
        {
            ViewBag.SystemCodeId = new SelectList(_context.systemCodes, "Id", "Description");
            return View();
        }

        // POST: SystemCodeDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SystemCodeDetail systemCodeDetail)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            systemCodeDetail.CreatedOn = DateTime.Now;
            systemCodeDetail.CreatedById = userId;
            _context.Add(systemCodeDetail);
            await _context.SaveChangesAsync();
            var activity = new AuditTrail
            {
                Action = "Criar",
                TimeStamp = DateTime.Now,
                IpAdress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserId = userId,
                Module = "SystemCodeDetails",
                AffectedTable = "SystemCodeDetails"
            };

            return RedirectToAction(nameof(Index));
            return View(systemCodeDetail);
        }

        // GET: SystemCodeDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemCodeDetail = await _context.systemCodeDetails.FindAsync(id);
            if (systemCodeDetail == null)
            {
                return NotFound();
            }
            ViewData["SystemCodeId"] = new SelectList(_context.systemCodes, "Id", "Description", systemCodeDetail.SystemCodeId);
            return View(systemCodeDetail);
        }

        // POST: SystemCodeDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SystemCodeDetail systemCodeDetail)
        {
            // Verifique os valores de id e systemCodeDetail.Id para garantir que eles são iguais
            Console.WriteLine($"id: {id}, systemCodeDetail.Id: {systemCodeDetail.Id}");

            if (id != systemCodeDetail.Id)
            {
                return NotFound();
            }
            var existingDetail = await _context.systemCodeDetails.FindAsync(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            existingDetail.Code = systemCodeDetail.Code; // Exemplo, substitua pelos campos reais
            existingDetail.Description = systemCodeDetail.Description; // idem
            existingDetail.ModifiedOn = DateTime.Now;
            existingDetail.ModifiedById = userId;
            existingDetail.OrderNo = systemCodeDetail.OrderNo;
            existingDetail.SystemCodeId = systemCodeDetail.SystemCodeId;
                _context.Entry(existingDetail).Property(x => x.CreatedById).IsModified = false;
                _context.Entry(existingDetail).Property(x => x.CreatedOn).IsModified = false;

            _context.Update(existingDetail);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
        }
        // GET: SystemCodeDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemCodeDetail = await _context.systemCodeDetails
                .Include(s => s.SystemCode)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemCodeDetail == null)
            {
                return NotFound();
            }

            return View(systemCodeDetail);
        }

        // POST: SystemCodeDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var systemCodeDetail = await _context.systemCodeDetails.FindAsync(id);
            if (systemCodeDetail != null)
            {
                _context.systemCodeDetails.Remove(systemCodeDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemCodeDetailExists(int id)
        {
            return _context.systemCodeDetails.Any(e => e.Id == id);
        }
    }
}
