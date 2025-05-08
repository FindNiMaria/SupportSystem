using HelpdeskSystem.Services;
using Microsoft.AspNetCore.Authorization;

namespace HelpdeskSystem.Authorization
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionService _permissionService;

        public PermissionAuthorizationHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            if (context.User == null)
                return;

            if (await _permissionService.HasPermissionAsync(context.User, requirement.PermissionCode))
            {
                context.Succeed(requirement);
            }
        }
    }
}