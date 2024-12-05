using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DishPlannerApp.Data;
using DishPlannerApp.Models;
using Microsoft.Extensions.Configuration;
using DishPlannerApp.Data.UserRepository;

namespace DishPlannerApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Access the configuration object directly from the builder
            IConfiguration configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IUserRepository, UserRepository>(); // Register the repository

            builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseAuthentication(); // Authentication middleware
            app.UseAuthorization();  // Authorization middleware

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddDbContext<ApplicationDbContext>(options => 
        //        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        //    services.AddScoped<IUserRepository, UserRepository>(); // Register the repository

        //}
    }
}
