using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DishPlannerApp.Data;
using DishPlannerApp.Models;
using Microsoft.Extensions.Configuration;
using DishPlannerApp.Data.UserRepository;
using DishPlannerApp.Data.RecipeRepository;
using Microsoft.AspNetCore.Authentication.Cookies;

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
            builder.Services.AddScoped<IUserRepository, UserRepository>(); 
            builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
            builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

            builder.Services.AddControllersWithViews();

            void ConfigureServices(IServiceCollection services)
            {
                services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                        .AddCookie(options =>
                        {
                            options.LoginPath = "/User/Login"; // Path to the login page
                            options.AccessDeniedPath = "/User/AccessDenied"; // Optional
                        });

                services.AddAuthorization();
                services.AddControllersWithViews();
            }

            void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    app.UseExceptionHandler("/Home/Error");
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseStaticFiles();

                app.UseRouting();

                app.UseAuthentication(); // Make sure this is added
                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                });
            }


            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User/Login"; // Correct path to your login action
                options.LogoutPath = "/User/Logout"; // If you add logout functionality later
                options.AccessDeniedPath = "/User/AccessDenied"; // Optional, for authorization failures
            });


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
            app.UseAuthentication(); // Authentication middleware
            app.UseAuthorization();  // Authorization middleware
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

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
