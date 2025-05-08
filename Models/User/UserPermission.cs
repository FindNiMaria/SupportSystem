// Models/UserPermission.cs
namespace HelpdeskSystem.Models.User
{
    public class UserPermission : UserActivity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
        public bool IsGranted { get; set; }
    }
}