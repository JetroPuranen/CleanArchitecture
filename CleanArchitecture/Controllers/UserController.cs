using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Model;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Controllers
{
    [ApiController]
    [Route("controller")]
    public class UserController : ControllerBase
    {
        private readonly IUserRegistrationService _userRegistrationService;

        public UserController(IUserRegistrationService userRegistrationService)
        {
            _userRegistrationService = userRegistrationService;
        }
        [HttpPost]
        public IActionResult RegisterUser(string name, string email)
        {

            _userRegistrationService.RegisterUserAsync(name, email);
            return Ok("Registration done");
        }
        [HttpPost("UserAsync")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] UserRegistrationRequest request)
        {
           
            var isExisting = await _userRegistrationService.EmailExistsAsync(request.Email);

            if (isExisting)
            {
                return BadRequest("Email is already in use");
            }
            var success = await _userRegistrationService.RegisterUserAsync(request.Name, request.Email);
            if (!success)
            {
                return BadRequest("Registration failed");
            }

            return Created();
        }
        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userRegistrationService.GetUserByEmailAsync(email);
            if (user == null)
                return NotFound(); // Voit palauttaa myös BadRequest tai muun vastaavan tilanteesta riippuen
            return Ok(user);
        }
    }
}
