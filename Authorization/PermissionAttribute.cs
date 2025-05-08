using Microsoft.AspNetCore.Authorization;

namespace HelpdeskSystem.Authorization
{
    public class PermissionAttribute : AuthorizeAttribute
    {
        public PermissionAttribute(string permissionCode)
            : base(PermissionAuthorizationPolicyProvider.PERMISSION_POLICY_PREFIX + permissionCode)
        {
        }
    }
}