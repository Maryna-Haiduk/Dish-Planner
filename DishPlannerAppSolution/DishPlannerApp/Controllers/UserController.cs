using DishPlannerApp.Data.UserRepository;
using DishPlannerApp.DTOs;
using DishPlannerApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DishPlannerApp.Controllers
{
    public class UserController : Controller 
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Display the registration form
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Process registration
        [HttpPost]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            if (!ModelState.IsValid)
                return View(userDto);

            if (await _userRepository.UserExistsAsync(userDto.Name))
            {
                ModelState.AddModelError("", "Username already exists.");
                return View(userDto);
            }

            var user = new User
            {
                UserName = userDto.Name,
                Email = userDto.Email
            };

            await _userRepository.RegisterAsync(user, userDto.Password);
            return RedirectToAction("Login");
        }

        // Display the login form
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Process login
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return View(loginDto);

            var user = await _userRepository.LoginAsync(loginDto.Name, loginDto.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(loginDto);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
