using helpdesk.Models;
using helpdesk.Models.DTO;

namespace helpdesk.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto dto);
        Task<string?> LoginAsync(LoginDto dto); 
    }
}
