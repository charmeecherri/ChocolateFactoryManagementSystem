
using ChocolateFactoryApi.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ChocolateFactoryApi.Data;
using Microsoft.EntityFrameworkCore;

namespace ChocolateFactoryApi.services
{
    public class AuthService: IAuthService
    {
        private readonly AppDbContext context;
        private readonly IConfiguration configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task<User> RegisterAsync(string name,string email, string username, string password, string role)
        {
            var passwordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.UTF8.GetBytes(username),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            var user = new User
            {
                Name =name,
                Email = email,
                UserName = username,
                PasswordHash = passwordHash,
                Role = role

            };
            
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }
        public async Task<User> LoginAsync(string username, string password, string role)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.UserName == username && u.Role == role);

            if (user == null)
            {
                return null;
            }

            var passwordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.UTF8.GetBytes(username),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            if (user.PasswordHash != passwordHash)
            {
                return null;
            }

            return user;
        }


        public async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}

