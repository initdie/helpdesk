using helpdesk.Models.Enums;

namespace helpdesk.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string HashPassword { get; set; } = string.Empty;

        public Role Role { get; set; } = Role.User;
    }
}
