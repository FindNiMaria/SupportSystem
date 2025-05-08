using System.ComponentModel;

namespace HelpdeskSystem.Models.User
{
    public class UserRole : UserActivity
    {
        public int Id { get; set; }

        [DisplayName("Usuário")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [DisplayName("Função")]
        public string RoleId { get; set; }
        public Role Role { get; set; }

        [DisplayName("Departamento")]
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}