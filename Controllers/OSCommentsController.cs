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
    public class OSCommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OSCommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OSComments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OSComments.Include(o => o.CriadoPor).Include(o => o.OS);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> OSComment(int Id)
        {
            var comments = await _context.OSComments_1.Where(x => x.IdOS == Id)
                .Include(c => c.CriadoPor)
                .Include(c => c.OS).ToListAsync();
            return View(comments);
        }
        // GET: OSComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oSComments = await _context.OSComments
                .Include(o => o.CriadoPor)
                .Include(o => o.OS)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oSComments == null)
            {
                return NotFound();
            }

            return View(oSComments);
        }

        // GET: OSComments/Create
        public IActionResult Create()
        {
            ViewData["CriadoPorId"] = new SelectList(_context.Users, "Id", "FullName");
            ViewData["IdOS"] = new SelectList(_context.OS, "Id", "Titulo");
            return View();
        }

        // POST: OSComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( OSComments oSComments)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            oSComments.CriadoEm = DateTime.Now;
            _context.Add(oSComments);
            await _context.SaveChangesAsync();
            //Registrar no Log de Auditoria

            var activity = new AuditTrail
            {
                Action = "Criar",
                TimeStamp = DateTime.Now,
                IpAdress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserId = userId,
                Module = "Comentário (OS)",
                AffectedTable = "Comentário (OS)"
            };
            _context.Add(activity);
            await _context.SaveChangesAsync();
            ViewData["CriadoPorId"] = new SelectList(_context.Users, "Id", "FullName", oSComments.CriadoPorId);
            ViewData["IdOS"] = new SelectList(_context.Tickets, "Id", "Titulo", oSComments.IdOS);
            return RedirectToAction(nameof(Index));

            
          
        }

        // GET: OSComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oSComments = await _context.OSComments.FindAsync(id);
            if (oSComments == null)
            {
                return NotFound();
            }
            ViewData["CriadoPorId"] = new SelectList(_context.Users, "Id", "FullName", oSComments.CriadoPorId);
            ViewData["IdOS"] = new SelectList(_context.OS, "Id", "Titulo", oSComments.IdOS);
            return View(oSComments);
        }

        // POST: OSComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OSComments oSComments)
        {
            if (id != oSComments.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oSComments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OSCommentsExists(oSComments.Id))
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
            ViewData["CriadoPorId"] = new SelectList(_context.Users, "Id", "FullName", oSComments.CriadoPorId);
            ViewData["IdOS"] = new SelectList(_context.OS, "Id", "Titulo", oSComments.IdOS);
            return View(oSComments);
        }

        // GET: OSComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oSComments = await _context.OSComments
                .Include(o => o.CriadoPor)
                .Include(o => o.OS)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oSComments == null)
            {
                return NotFound();
            }

            return View(oSComments);
        }

        // POST: OSComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oSComments = await _context.OSComments.FindAsync(id);
            if (oSComments != null)
            {
                _context.OSComments.Remove(oSComments);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OSCommentsExists(int id)
        {
            return _context.OSComments.Any(e => e.Id == id);
        }
    }
}
