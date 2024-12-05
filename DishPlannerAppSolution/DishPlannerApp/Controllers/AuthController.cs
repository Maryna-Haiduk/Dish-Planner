﻿using DishPlannerApp.Data.UserRepository;
using DishPlannerApp.Models;
using Microsoft.AspNetCore.Mvc;
using DishPlannerApp.DTOs;

namespace DishPlannerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register (UserDto userDto)
        {
            if (await _userRepository.UserExistsAsync(userDto.Name))
                return BadRequest("Username already exists.");

            var user = new User
            {
                UserName = userDto.Name,
                Email = userDto.Email
            };

            await _userRepository.RegisterAsync(user, userDto.Password);
            return Ok("Registration successful");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            var user = await _userRepository.LoginAsync(loginDto.Name, loginDto.Password);
            if (user == null)
                return Unauthorized("Invalid username or password");

            return Ok("Login successful");
        }
    }
}
