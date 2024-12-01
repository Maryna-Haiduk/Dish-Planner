using Microsoft.AspNetCore.Identity;

namespace DishPlannerApp.Models
{
    public class User : IdentityUser
    {
        private int UserId { get; set; }    
        private string UserName {  get; set; }
        private string UserEmail { get; set; } = string.Empty;
        private string Password { get; set; }
    }
}
