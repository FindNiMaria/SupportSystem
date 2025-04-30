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
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Comment_1.Include(c => c.CriadoPor).Include(c => c.Ticket);
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: Comments Criar
        public async Task<IActionResult> TicketComment(int Id)
        {
            var comments = await _context.Comment_1.Where(x=>x.IdChamado == Id)
                .Include(c => c.CriadoPor)
                .Include(c => c.Ticket).ToListAsync();
            return View(comments);
        }
        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment_1
                .Include(c => c.CriadoPor)
                .Include(c => c.Ticket)
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
            ViewData["IdChamado"] = new SelectList(_context.Tickets, "Id", "Titulo");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Comment comment)
        {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                comment.CriadoPorId = userId;
                comment.CriadoEm = DateTime.Now;
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
                AffectedTable = "Comentário"
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

            var comment = await _context.Comment_1.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["CriadoPorId"] = new SelectList(_context.Users, "Id", "FullName", comment.CriadoPorId);
            ViewData["IdChamado"] = new SelectList(_context.Tickets, "Id", "Titulo", comment.IdChamado);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Comment comment)
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
                        AffectedTable = "Comentários"
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
            ViewData["CriadoPorId"] = new SelectList(_context.Users, "Id", "FullName", comment.CriadoPorId);
            ViewData["IdChamado"] = new SelectList(_context.Tickets, "Id", "Titulo", comment.IdChamado);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment_1
                .Include(c => c.CriadoPor)
                .Include(c => c.Ticket)
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
                AffectedTable = "Comentários"
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
            var comment = await _context.Comment_1.FindAsync(id);
            if (comment != null)
            {
                _context.Comment_1.Remove(comment);
            }

            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(int id)
        {
            return _context.Comment_1.Any(e => e.Id == id);
        }
    }
}
