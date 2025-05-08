// Services/IPermissionService.cs
using HelpdeskSystem.Models.User;
using System.Security.Claims;

namespace HelpdeskSystem.Services
{
    public interface IPermissionService
    {
        Task<bool> HasPermissionAsync(ClaimsPrincipal user, string permissionCode);
        Task<bool> HasPermissionAsync(string userId, string permissionCode);
        Task<IEnumerable<Permission>> GetUserPermissionsAsync(string userId);
        Task<IEnumerable<Role>> GetUserRolesAsync(string userId);
        Task<bool> GrantPermissionToUserAsync(string userId, string permissionCode);
        Task<bool> RevokePermissionFromUserAsync(string userId, string permissionCode);
        Task<bool> AssignRoleToUserAsync(string userId, string roleId, int? departmentId = null);
        Task<bool> RemoveRoleFromUserAsync(string userId, string roleId);
    }
}
