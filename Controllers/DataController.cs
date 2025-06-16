using HelpdeskSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelpdeskSystem.Controllers
{
    public class DataController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DataController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<JsonResult> GetTicketSubCategories(int Id)
        {
            try
            {
                var subcategories = await _context
                    .TicketSubCategories
                    .Where(x => x.CategoryId == Id)
                    .OrderBy(c => c.Name)
                    .Select(i => new { id = i.Id, name = i.Name })
                    .Distinct()
                    .ToListAsync();

                return Json(subcategories);
            }
            catch (Exception)
            {
                return Json(new { });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<JsonResult> GetCategoriaPrioridade(int categoriaId)
        {
            try
            {
                var categoria = await _context.TicketCategories
                    .Include(c => c.DefautPriority)
                    .FirstOrDefaultAsync(c => c.Id == categoriaId);

                if (categoria?.DefautPriority != null)
                {
                    return Json(new { id = categoria.DefautPriority.Id, descricao = categoria.DefautPriority.Description });
                }

                return Json(new { });
            }
            catch (Exception)
            {
                return Json(new { });
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<JsonResult> GetOSSubCategories(int Id)
        {
            try
            {
                var subcategories = await _context
                    .OSSubCategories
                    .Where(x => x.CategoryId == Id)
                    .OrderBy(c => c.Name)
                    .Select(i => new { id = i.Id, name = i.Name })
                    .Distinct()
                    .ToListAsync();

                return Json(subcategories);
            }
            catch (Exception)
            {
                return Json(new { });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<JsonResult> GetOSCategoriaPrioridade(int categoriaId)
        {
            try
            {
                var categoria = await _context.OSCategories
                    .Include(c => c.DefaultPriority)
                    .FirstOrDefaultAsync(c => c.Id == categoriaId);

                if (categoria?.DefaultPriority != null)
                {
                    return Json(new { id = categoria.DefaultPriority.Id, descricao = categoria.DefaultPriority.Description });
                }

                return Json(new { });
            }
            catch (Exception)
            {
                return Json(new { });
            }
        }
    }
}
