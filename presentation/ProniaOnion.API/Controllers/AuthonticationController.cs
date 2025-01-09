
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.DTOs.AppUsers;

namespace ProniaOnion.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthonticationController : ControllerBase
    {
        private readonly IAuthenticationService _service;

        public AuthonticationController(IAuthenticationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm]RegisterDto userDto)
        {
           await _service.RegisterAsync(userDto);
            return NoContent();
        }
    }
}
