using HelpdeskSystem.Data;
using HelpdeskSystem.Models;
using HelpdeskSystem.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HelpdeskSystem.Controllers.User
{
    [Authorize(Roles = "Administrador")]
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
            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ApplicationUser user, string password)
        {
            try
            {
                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

                ApplicationUser registeredUser = new()
                {
                    FirstName = user.FirstName,
                    UserName = user.UserName,
                    LastName = user.LastName,
                    Email = user.Email,
                    EmailConfirmed = true,
                    PhoneNumber = user.PhoneNumber,
                    Gender = user.Gender,
                    Country = user.Country,
                    City = user.City,
                    DepartmentId = user.DepartmentId,
                    Role = user.Role
                };

                var result = await _userManager.CreateAsync(registeredUser, password);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(user.Role))
                    {
                        await _userManager.AddToRoleAsync(registeredUser, user.Role);
                    }

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
                    ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
                    ViewBag.Roles = _rolemanager.Roles.Select(r => new SelectListItem
                    {
                        Value = r.Name,
                        Text = r.Name
                    }).ToList();
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
                ViewBag.Roles = _rolemanager.Roles.Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                }).ToList();
                return View(user);
            }
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ApplicationUser model, string NewPassword, string Role)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            // Atualizar dados básicos
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Country = model.Country;
            user.City = model.City;
            user.Gender = model.Gender;
            user.DepartmentId = model.DepartmentId;
            user.Role = model.Role;

            // Atualizar senha se fornecida
            if (!string.IsNullOrEmpty(NewPassword))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, NewPassword);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);

                    ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name", model.DepartmentId);
                    ViewBag.Roles = new SelectList(_rolemanager.Roles, "Name", "Name", Role);
                    return View(model);
                }
            }

            // Atualizar Role
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!string.IsNullOrEmpty(Role))
                await _userManager.AddToRoleAsync(user, Role);

            // Salvar alterações
            var updateResult = await _userManager.UpdateAsync(user);
            if (updateResult.Succeeded)
                return RedirectToAction(nameof(Index));

            foreach (var error in updateResult.Errors)
                ModelState.AddModelError("", error.Description);

            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name", model.DepartmentId);
            ViewBag.Roles = new SelectList(_rolemanager.Roles, "Name", "Name", Role);
            return View(model);
        }




        // GET: UsersController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name", user.DepartmentId);
            ViewBag.Roles = new SelectList(_rolemanager.Roles, "Name", "Name", (await _userManager.GetRolesAsync(user)).FirstOrDefault());

            return View(user);
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
