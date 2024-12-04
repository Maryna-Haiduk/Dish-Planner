using DishPlannerApp.Models;

namespace DishPlannerApp.Data.UserRepository
{
    public interface IUserRepository
    {
        Task<bool> RegisterAsync(User user, string password);
        Task<User> LoginAsync(string username, string password);
        Task<bool> UserExistsAsync(string username);
    }
}
