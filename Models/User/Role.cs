using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace HelpdeskSystem.Models.User
{
    public class Role : IdentityRole
    {
        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Permissões")]
        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}