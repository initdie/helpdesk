using helpdesk.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace helpdesk
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _dbService;
        public AuthService(AppDbContext context)
        {
            _dbService = context;
        }
        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            if(await _dbService.Users.AnyAsync(u => u.Email == dto.Email))
                return false;

            var user = new Models.User
            {
                Email = dto.Email,
                HashPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role
            };
            _dbService.Users.Add(user);
            await _dbService.SaveChangesAsync();
            return true;
        }
    }
}
