using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DishPlannerApp.Models;

namespace DishPlannerApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Menu> Menus { get; set; }
    }
}
