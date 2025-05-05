using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace HelpdeskSystem.Models
{
    public class Role : IdentityRole
    {
        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Permissões")]
        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}