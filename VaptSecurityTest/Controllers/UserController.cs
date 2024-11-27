using Microsoft.AspNetCore.Mvc;
using VaptSecurityTest.Models;
using VaptSecurityTest.Services;

namespace VaptSecurityTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        //[HttpPost("register")]
        //public async Task<IActionResult> Register([FromBody] UserInputModel userInput)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    await _userService.RegisterAsync(userInput.Username, userInput.Password);
        //    return Ok("User registered successfully");
        //}

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserInputModel userInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.RegisterAsync(userInput.Username, userInput.Password);

            if (!result)
            {
                return Conflict(new { message = "Username already exists. Please choose a different username." });
            }

            return Ok(new { message = "Registration successful!" });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserInputModel userInput)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isAuthenticated = await _userService.LoginAsync(userInput.Username, userInput.Password);
            if (!isAuthenticated)
                return Unauthorized("Invalid credentials");

            return Ok("Login successful");
        }
    }
}
