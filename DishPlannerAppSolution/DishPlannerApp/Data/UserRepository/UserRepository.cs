using DishPlannerApp.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace DishPlannerApp.Data.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Registration Logic
        public async Task<bool> RegisterAsync(User user, string password)
        {
            // Hash the password
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            user.PasswordHash = passwordHash;

            // Add to the database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        // Login Logic
        public async Task<User> LoginAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null) return null;

            // Verify the password
            if(BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) 
                return user;

            return null;
        }

        // Check if the user exists
        public async Task<bool> UserExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.UserName == username);
        }
    }
}
