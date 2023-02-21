using domain.Exceptions;
using domain.interfaces.repositories;
using domain.interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using userContext.domain.entities;

namespace domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await _userRepository.GetUserByUsername(username);

            if (user != null && _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password + user.PasswordSalt) == PasswordVerificationResult.Success)
            {
                user.Token = GenerateToken();
                await _userRepository.UpdateUser(user);
                return user;
            }

            return null;
        }

        public async Task<User> Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (await _userRepository.GetUserByUsername(user.Username) != null)
                throw new AppException("Username \"" + user.Username + "\" is already taken");

            user.PasswordSalt = GenerateSalt();
            user.PasswordHash = _passwordHasher.HashPassword(user, password + user.PasswordSalt);
            await _userRepository.CreateUser(user);

            return user;
        }

        public async Task ChangePassword(User user, string oldPassword, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(oldPassword))
                throw new AppException("Old password is required");

            if (string.IsNullOrWhiteSpace(newPassword))
                throw new AppException("New password is required");

            if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, oldPassword + user.PasswordSalt) != PasswordVerificationResult.Success)
                throw new AppException("Old password is incorrect");

            user.PasswordSalt = GenerateSalt();
            user.PasswordHash = _passwordHasher.HashPassword(user, newPassword + user.PasswordSalt);
            await _userRepository.UpdateUser(user);
        }

        private string GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        private string GenerateToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("your-secret-key");
            User user = new User();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

}
