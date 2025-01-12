using EShop.Service.Abstract;
using EShop.Shared.Dtos.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers
{
    //uygulamaadresi/api/auths
    // bu şekilde bir rota belirlediğimizde bu adrese;
    // post metodu ile istek geldiğinde httppost tipindeki metod çalışacak
    //get metodu ile bir istek geldiğinde httpget tipindeki metod çalışacak
    [Route("api/auths")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _authService.LoginAsycn(loginDto);
            return StatusCode(result.StatusCode,result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await _authService.RegisterAync(registerDto);
            return StatusCode(result.StatusCode, result);
        }
    }
}
