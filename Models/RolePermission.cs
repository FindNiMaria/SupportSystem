namespace HelpdeskSystem.Models
{
    public class RolePermission : UserActivity
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        public Role Role { get; set; }
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}