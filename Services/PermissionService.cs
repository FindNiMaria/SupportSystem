using HelpdeskSystem.Data;
using HelpdeskSystem.Models;
using HelpdeskSystem.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HelpdeskSystem.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermissionService(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> HasPermissionAsync(ClaimsPrincipal user, string permissionCode)
        {
            var userId = _userManager.GetUserId(user);
            if (string.IsNullOrEmpty(userId))
                return false;

            return await HasPermissionAsync(userId, permissionCode);
        }

        public async Task<bool> HasPermissionAsync(string userId, string permissionCode)
        {
            // Verificar permissões diretas do usuário
            var directPermission = await _context.UserPermissions
                .Include(up => up.Permission)
                .FirstOrDefaultAsync(up => up.UserId == userId && up.Permission.Code == permissionCode);

            if (directPermission != null)
            {
                // Se existe uma permissão direta e IsGranted é false, bloqueia mesmo que o papel tenha a permissão
                if (!directPermission.IsGranted)
                    return false;

                // Se existe uma permissão direta e IsGranted é true, permite
                if (directPermission.IsGranted)
                    return true;
            }

            // Verificar permissões baseadas em papéis
            var rolePermission = await (
                from ur in _context.UserRoles
                join rp in _context.RolePermissions on ur.RoleId equals rp.RoleId
                join p in _context.Permissions on rp.PermissionId equals p.Id
                where ur.UserId == userId && p.Code == permissionCode
                select p
            ).AnyAsync();

            return rolePermission;
        }

        public async Task<IEnumerable<Permission>> GetUserPermissionsAsync(string userId)
        {
            // Obter todas as permissões do usuário (diretas e baseadas em papéis)
            var rolePermissions = await (
                from ur in _context.UserRoles
                join rp in _context.RolePermissions on ur.RoleId equals rp.RoleId
                join p in _context.Permissions on rp.PermissionId equals p.Id
                where ur.UserId == userId
                select p
            ).ToListAsync();

            var directPermissions = await _context.UserPermissions
                .Include(up => up.Permission)
                .Where(up => up.UserId == userId && up.IsGranted)
                .Select(up => up.Permission)
                .ToListAsync();

            // Combinar e remover duplicatas
            return rolePermissions.Union(directPermissions, new PermissionEqualityComparer());
        }

        public async Task<IEnumerable<Role>> GetUserRolesAsync(string userId)
        {
            var userRoles = await _context.UserRoles
                .Include(ur => ur.Role)
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role)
                .ToListAsync();

            return userRoles;
        }

        public async Task<bool> GrantPermissionToUserAsync(string userId, string permissionCode)
        {
            var permission = await _context.Permissions.FirstOrDefaultAsync(p => p.Code == permissionCode);
            if (permission == null)
                return false;

            var userPermission = await _context.UserPermissions
                .FirstOrDefaultAsync(up => up.UserId == userId && up.PermissionId == permission.Id);

            if (userPermission == null)
            {
                // Adicionar nova permissão ao usuário
                var currentUser = _httpContextAccessor.HttpContext?.User;
                var currentUserId = currentUser != null ? _userManager.GetUserId(currentUser) : null;

                userPermission = new UserPermission
                {
                    UserId = userId,
                    PermissionId = permission.Id,
                    IsGranted = true,
                    CreatedById = currentUserId,
                    CreatedOn = DateTime.Now
                };

                _context.UserPermissions.Add(userPermission);
            }
            else
            {
                // Atualizar permissão existente
                userPermission.IsGranted = true;
                userPermission.ModifiedById = _userManager.GetUserId(_httpContextAccessor.HttpContext?.User);
                userPermission.ModifiedOn = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RevokePermissionFromUserAsync(string userId, string permissionCode)
        {
            var permission = await _context.Permissions.FirstOrDefaultAsync(p => p.Code == permissionCode);
            if (permission == null)
                return false;

            var userPermission = await _context.UserPermissions
                .FirstOrDefaultAsync(up => up.UserId == userId && up.PermissionId == permission.Id);

            if (userPermission == null)
            {
                // Adicionar negação explícita da permissão
                var currentUser = _httpContextAccessor.HttpContext?.User;
                var currentUserId = currentUser != null ? _userManager.GetUserId(currentUser) : null;

                userPermission = new UserPermission
                {
                    UserId = userId,
                    PermissionId = permission.Id,
                    IsGranted = false,
                    CreatedById = currentUserId,
                    CreatedOn = DateTime.Now
                };

                _context.UserPermissions.Add(userPermission);
            }
            else
            {
                // Atualizar permissão existente
                userPermission.IsGranted = false;
                userPermission.ModifiedById = _userManager.GetUserId(_httpContextAccessor.HttpContext?.User);
                userPermission.ModifiedOn = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AssignRoleToUserAsync(string userId, string roleId, int? departmentId = null)
        {
            var existingUserRole = await _context.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId &&
                                       (departmentId == null || ur.DepartmentId == departmentId));

            if (existingUserRole != null)
                return true; // Já existe

            var currentUser = _httpContextAccessor.HttpContext?.User;
            var currentUserId = currentUser != null ? _userManager.GetUserId(currentUser) : null;

            var userRole = new UserRole
            {
                UserId = userId,
                RoleId = roleId,
                DepartmentId = departmentId,
                CreatedById = currentUserId,
                CreatedOn = DateTime.Now
            };

            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveRoleFromUserAsync(string userId, string roleId)
        {
            var userRoles = await _context.UserRoles
                .Where(ur => ur.UserId == userId && ur.RoleId == roleId)
                .ToListAsync();

            if (!userRoles.Any())
                return false;

            _context.UserRoles.RemoveRange(userRoles);
            await _context.SaveChangesAsync();
            return true;
        }
    }

    // Classe auxiliar para comparar permissões (evitar duplicatas)
    public class PermissionEqualityComparer : IEqualityComparer<Permission>
    {
        public bool Equals(Permission x, Permission y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Permission obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}