using Azure.Core;
using LicenseHubWebAPI.DataAccess.Context;
using LicenseHubWebAPI.Domain.DTOs;
using LicenseHubWebAPI.Domain.Entities;
using LicenseHubWebAPI.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LicenseHubWebAPI.DataAccess.Implementation
{
    public class AuthRepository : IAuthRepository
    {
        private readonly LicenseHubDbContext _context;
        private readonly IConfiguration _configuration;
        private static UserResponseDTO? _loggedUser = null;

        public AuthRepository(LicenseHubDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<UserResponseDTO?> Register(UserRegisterDTO request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new User
            {
                Username = request.UserName,
                UserEmail = request.UserEmail,
                PasswordHashed = passwordHash,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var newUser = await GetSingleUser(user.UserEmail);
            if (newUser == null)
            {
                return null;
            }

            string sessionToken = CreateToken(newUser);

            var createdUser = new UserResponseDTO()
            {
                Name = newUser.Username,
                UserName = newUser.UserEmail,
            };

            _loggedUser = createdUser;

            return createdUser;
        }

        public async Task<UserResponseDTO?> GetUser(string userEmail)
        {
            var user = await GetSingleUser(userEmail);
            if (user == null)
            {
                return null;
            }

            var response = new UserResponseDTO() 
            { 
                Name = user.Username, UserName = user.UserEmail 
            };

            return response;
        }

        public async Task<UserResponseDTO?> Login(UserRequestDTO request)
        {
            var user = await GetSingleUser(request.UserEmail);

            if (user == null)
            {
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHashed))
            {
                return null;
            }

            string sessionToken = CreateToken(user);

            var userDetails = new UserResponseDTO()
            {
                Name = user.Username,
                UserName = user.UserEmail,
            };

            _loggedUser = userDetails;

            return userDetails;
        }

        private async Task<User?> GetSingleUser(string userEmail)
        {
            var user = await _context.Users.Where(u => u.UserEmail == userEmail).FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            return user;
        }

        private string CreateToken(User user)
        {
            // use user object to set the username as the claim to the token
            List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username)
        };

            // grab the key from appsetiing.json
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            // signing credentials
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // the token
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: creds
                );

            // write the token
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
