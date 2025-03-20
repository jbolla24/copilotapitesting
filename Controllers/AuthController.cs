using JwtAuthApi.Data;
using JwtAuthApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JwtAuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;

        public AuthController(AppDbContext context, TokenService tokenService) {
            _context = context;
            _tokenService = tokenService;
           
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] Models.User login) {
            var user = _context.Users.FirstOrDefault(u => u.Name == login.Name && u.Password == login.Password);

            //if (user == null)
                //return Unauthorized("Invalid Credentails");

            var token = _tokenService.GenerateToken(user);
            return Ok(new { token });
        }
        [HttpPost("register")]

        public IActionResult Register([FromBody] Models.User newUser) {
            if (_context.Users.Any(u => u.Name == newUser.Name))
                return BadRequest("User Already Exists");
            _context.Users.Add(newUser);
            _context.SaveChanges();

            var users = _context.Users.ToList();
            return Ok(users);
        }
        [Authorize]
        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }
    }
}
