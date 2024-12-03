using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DishPlannerApp.Models;


namespace DishPlannerApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Add DbSets for your models here
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Menu> Menus { get; set; }
    }
}

