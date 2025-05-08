using HelpdeskSystem.Data;
using HelpdeskSystem.Models;
using HelpdeskSystem.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HelpdeskSystem.Controllers.User
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> rolemanager,SignInManager<ApplicationUser> signInManager)
        {
            _rolemanager = rolemanager;
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }
        // GET: UsersController
        public async Task<ActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();

            return View(users);
        }

        // GET: UsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ApplicationUser user)
        {
            try
            {
                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                ApplicationUser registereduser = new();
                registereduser.FirstName = user.FirstName;
                registereduser.UserName = user.UserName;
                registereduser.LastName = user.LastName;
                registereduser.NormalizedUserName = user.UserName;
                registereduser.Email = user.Email;
                registereduser.EmailConfirmed = true;
                registereduser.PhoneNumber = user.PhoneNumber;
                registereduser.Gender = user.Gender;
                registereduser.Country = user.Country;
                registereduser.City = user.City;
                var result = await _userManager.CreateAsync(registereduser, user.PasswordHash);
                if(result.Succeeded)
                {
                    //Registrar no Log de Auditoria

                    var activity = new AuditTrail
                    {
                        Action = "Criar",
                        TimeStamp = DateTime.Now,
                        IpAdress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                        UserId = userid,
                        Module = "Usuários",
                        AffectedTable = "Usuários"
                    };

                    _context.Add(activity);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }

            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: UsersController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            // Obter o usuário atual (pode ser feito com User.Identity.Name ou um Id específico)
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            return View(user); // Passa o usuário para a view
        }

        // POST: UsersController/Edit/5
        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(string id, ApplicationUser userModel, string currentPassword, string newPassword, string confirmNewPassword)
{
    // Encontra o usuário pelo ID
    var user = await _userManager.FindByIdAsync(id.ToString());
    if (user == null)
    {
        return NotFound(); // Se o usuário não for encontrado
    }

    // Atualiza as informações do usuário
    user.FirstName = userModel.FirstName;
    user.LastName = userModel.LastName;
    user.Email = userModel.Email;
    user.PhoneNumber = userModel.PhoneNumber;
    user.Country = userModel.Country;
    user.City = userModel.City;
    user.Gender = userModel.Gender;

    // Verifica se o usuário deseja alterar a senha
    if (!string.IsNullOrEmpty(currentPassword) && !string.IsNullOrEmpty(newPassword) && newPassword == confirmNewPassword)
    {
        // Tenta alterar a senha
        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        if (!result.Succeeded)
        {
            // Se houver erros ao tentar alterar a senha
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(userModel); // Retorna para a view com os erros de senha
        }
    }

    // Salva as alterações no banco de dados
    var updateResult = await _userManager.UpdateAsync(user);
    if (updateResult.Succeeded)
    {
        return RedirectToAction(nameof(Index)); // Redireciona para a lista ou qualquer outra página desejada
    }

    // Caso as atualizações falhem, retorna com mensagens de erro
    foreach (var error in updateResult.Errors)
    {
        ModelState.AddModelError(string.Empty, error.Description);
    }

    return View(userModel); // Retorna para a view com os erros
}


        // GET: UsersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
