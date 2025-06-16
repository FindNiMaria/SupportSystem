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
using HelpdeskSystem.Models.SO;
using HelpdeskSystem.Models.User;
using HelpdeskSystem.ViewModels;

namespace HelpdeskSystem.Controllers.SO
{
    public class OSCommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OSCommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Comments
        public async Task<IActionResult> Index(OSCommentsViewModel vm)
        {
            var query = _context.OSComment_1
                .Include(c => c.CreatedBy)
                .Include(c => c.OS)
                .AsQueryable();

            if (vm.OSId.HasValue)
                query = query.Where(c => c.OSId == vm.OSId);

            if (!string.IsNullOrWhiteSpace(vm.CreatedById))
                query = query.Where(c => c.CreatedById == vm.CreatedById);

            if (!string.IsNullOrWhiteSpace(vm.DescriptionKeyword))
                query = query.Where(c => c.Description.Contains(vm.DescriptionKeyword));

            if (vm.CreatedFrom.HasValue)
                query = query.Where(c => c.CreatedOn >= vm.CreatedFrom.Value);

            if (vm.CreatedTo.HasValue)
                query = query.Where(c => c.CreatedOn <= vm.CreatedTo.Value);

            vm.Comments = await query.OrderByDescending(c => c.CreatedOn).ToListAsync();

            vm.Ordens = new SelectList(_context.OS, "Id", "Title");
            vm.Users = new SelectList(_context.Users, "Id", "FullName");

            return View(vm);
        }
        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.OSComment_1
                .Include(c => c.CreatedBy)
                .Include(c => c.OS)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        public IActionResult Create()
        {
            ViewData["CriadoPorId"] = new SelectList(_context.Users, "Id", "FullName");
            ViewData["IdOS"] = new SelectList(_context.OS, "Id", "Titulo");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OSComment comment)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            comment.CreatedById = userId;
            comment.CreatedOn = DateTime.Now;
            _context.Add(comment);
            await _context.SaveChangesAsync();
            //Registrar no Log de Auditoria

            var activity = new AuditTrail
            {
                Action = "Criar",
                TimeStamp = DateTime.Now,
                IpAdress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserId = userId,
                Module = "Comentário",
                AffectedTable = "OS"
            };
            _context.Add(activity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.OSComment_1.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["CriadoPorId"] = new SelectList(_context.Users, "Id", "FullName", comment.CreatedById);
            ViewData["IdOS"] = new SelectList(_context.OS, "Id", "Titulo", comment.OSId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OSComment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                    //Registrar no Log de Auditoria
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var activity = new AuditTrail
                    {
                        Action = "Editar",
                        TimeStamp = DateTime.Now,
                        IpAdress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                        UserId = userId,
                        Module = "Comentários",
                        AffectedTable = "OS"
                    };
                    _context.Add(activity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
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
            ViewData["CriadoPorId"] = new SelectList(_context.Users, "Id", "FullName", comment.CreatedById);
            ViewData["IdOS"] = new SelectList(_context.OS, "Id", "Titulo", comment.OSId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.OSComment_1
                .Include(c => c.CreatedBy)
                .Include(c => c.OS)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }
            //Registro de auditoria
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var activity = new AuditTrail
            {
                Action = "Deletar",
                TimeStamp = DateTime.Now,
                IpAdress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserId = userId,
                Module = "Comentários",
                AffectedTable = "OS"
            };
            _context.Add(activity);
            await _context.SaveChangesAsync();
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _context.OSComment_1.FindAsync(id);
            if (comment != null)
            {
                _context.OSComment_1.Remove(comment);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(int id)
        {
            return _context.OSComment_1.Any(e => e.Id == id);
        }
    }
}
