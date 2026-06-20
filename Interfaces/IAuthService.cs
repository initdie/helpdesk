using helpdesk.Models;

namespace helpdesk.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto dto);
    }
}
