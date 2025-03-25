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
    public class OSController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OSController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OS
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OS.Include(o => o.CriadoPor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oS = await _context.OS
                .Include(o => o.CriadoPor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oS == null)
            {
                return NotFound();
            }

            return View(oS);
        }

        // GET: OS/Create
        public IActionResult Create()
        {
            ViewData["CriadoPorId"] = new SelectList(_context.Users, "Id", "FullName");
            return View();
        }

        // POST: OS/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OS oS)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            oS.CriadoEm = DateTime.Now;
            oS.CriadoPorId = userId;
            _context.Add(oS);
            await _context.SaveChangesAsync();
            //Registrar no Log de Auditoria

            var activity = new AuditTrail
            {
                Action = "Criar",
                TimeStamp = DateTime.Now,
                IpAdress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserId = userId,
                Module = "OS",
                AffectedTable = "OS"
            };
            _context.Add(activity);
            await _context.SaveChangesAsync();
            ViewData["CriadoPorId"] = new SelectList(_context.Users, "Id", "FullName", oS.CriadoPorId);
            return RedirectToAction(nameof(Index));
        }

        // GET: OS/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oS = await _context.OS.FindAsync(id);
            if (oS == null)
            {
                return NotFound();
            }
            ViewData["CriadoPorId"] = new SelectList(_context.Users, "Id", "Id", oS.CriadoPorId);
            return View(oS);
        }

        // POST: OS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OS oS)
        {
            if (id != oS.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oS);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OSExists(oS.Id))
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
            
            ViewData["CriadoPorId"] = new SelectList(_context.Users, "Id", "Id", oS.CriadoPorId);
            return View(oS);
        }

        // GET: OS/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oS = await _context.OS
                .Include(o => o.CriadoPor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oS == null)
            {
                return NotFound();
            }

            return View(oS);
        }

        // POST: OS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oS = await _context.OS.FindAsync(id);
            if (oS != null)
            {
                _context.OS.Remove(oS);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OSExists(int id)
        {
            return _context.OS.Any(e => e.Id == id);
        }
    }
}
