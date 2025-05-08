using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace HelpdeskSystem.Authorization
{
    public class PermissionAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public const string PERMISSION_POLICY_PREFIX = "Permission_";

        public PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
            : base(options)
        {
        }

        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            // Verificar se o nome da política começa com o prefixo de permissão
            if (policyName.StartsWith(PERMISSION_POLICY_PREFIX, StringComparison.OrdinalIgnoreCase))
            {
                var permissionCode = policyName.Substring(PERMISSION_POLICY_PREFIX.Length);
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new PermissionRequirement(permissionCode));
                return policy.Build();
            }

            // Caso contrário, usar o provedor padrão
            return await base.GetPolicyAsync(policyName);
        }
    }
}
