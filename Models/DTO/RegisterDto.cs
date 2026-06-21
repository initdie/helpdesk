using helpdesk.Models.Enums;

namespace helpdesk.Models.DTO
{
    public record RegisterDto(string Email, string Password, Role Role);
}