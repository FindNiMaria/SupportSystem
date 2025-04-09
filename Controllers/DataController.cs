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
                    .Where(x => x.CategoriaId == Id)
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
                    .Include(c => c.PrioridadePadrao)
                    .FirstOrDefaultAsync(c => c.Id == categoriaId);

                if (categoria?.PrioridadePadrao != null)
                {
                    return Json(new { id = categoria.PrioridadePadrao.Id, descricao = categoria.PrioridadePadrao.Descricao });
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
