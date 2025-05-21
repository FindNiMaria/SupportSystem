using System.Security.Claims;

public static class PermissionHelper
{
        public static bool IsAdmin(ClaimsPrincipal user)
        {
            return user.IsInRole("Administrador");
        }
        public static bool IsTecnico(ClaimsPrincipal user)
        {
            return user.IsInRole("Técnico");
        }
        public static bool IsCoordenador(ClaimsPrincipal user)
        {
            return user.IsInRole("Coordenador");
        }
}
