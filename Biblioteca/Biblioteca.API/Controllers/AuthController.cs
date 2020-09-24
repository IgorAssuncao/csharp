using Biblioteca.Models;
using Biblioteca.Services;
using Biblioteca.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Biblioteca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        public AuthService AuthService { get; set; }

        public AuthController(AuthService authService)
        {
            AuthService = authService;
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginRequest loginRequest)
        {
            LoginResponse authRes = await AuthService.Authenticate(loginRequest.Email, loginRequest.Password);
            if (!authRes.Status)
                return BadRequest();

            return Ok(authRes);
        }
    }
}
