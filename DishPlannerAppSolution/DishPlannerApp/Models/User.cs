using Microsoft.AspNetCore.Identity;

namespace DishPlannerApp.Models
{
    public class User : IdentityUser
    {
        public int UserId { get; set; }    
        public string UserName {  get; set; }
        public string UserEmail { get; set; }
        public string PasswordHash{ get; set; }

    }
}
