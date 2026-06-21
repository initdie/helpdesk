using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using helpdesk.Interfaces;
using helpdesk.Models.DTO;
using helpdesk.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace helpdesk.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _dbService;
        private readonly IConfiguration _config;
        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _dbService = context;
            _config = configuration;
        }
        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            if(await _dbService.Users.AnyAsync(u => u.Email == dto.Email))
                return false;

            var user = new User
            {
                Email = dto.Email,
                HashPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role
            };
            _dbService.Users.Add(user);
            await _dbService.SaveChangesAsync();
            return true;
        }

        public async Task<string?> LoginAsync(LoginDto dto)
        {
            var user = await _dbService.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if(user == null) return null;
            if(!BCrypt.Net.BCrypt.Verify(dto.Password, user.HashPassword))
                return null;

            return GenerateToken(user);
        }

        private string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // id агента
                new Claim(ClaimTypes.Role, user.Role.ToString())          // роль
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        } 
    }
}
