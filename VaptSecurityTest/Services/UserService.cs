using Microsoft.EntityFrameworkCore;
using VaptSecurityTest.Data;
using VaptSecurityTest.Models;

namespace VaptSecurityTest.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task RegisterAsync(string username, string password)
        //{
        //    var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        //    var user = new User { Username = username, PasswordHash = passwordHash };
        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();
        //}

        public async Task<bool> RegisterAsync(string username, string password)
        {
            // Check if the username already exists
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (existingUser != null)
            {
                // Username already exists
                return false;
            }

            // Hash the password (ensure hashing logic is implemented elsewhere)
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            // Add new user
            var user = new User
            {
                Username = username,
                PasswordHash = hashedPassword
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return false;

            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }
    }
}
