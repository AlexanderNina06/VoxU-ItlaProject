using Microsoft.AspNetCore.Mvc;
using VoxU_Backend.Core.Application.DTOS.Account;
using VoxU_Backend.Core.Application.Interfaces.Services;
using VoxU_Backend.Helpers;

namespace VoxU_Backend.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("authenticate")] //Luego de definir el tipo de verbo, podemos customizar la ruta hacia el endpoint como lo vemos aqui 
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _accountService.AuthenticateAsync(request));
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromForm]RegisterRequest request)
        {
            //Converting Image to bytes
            request.ProfilePicture = ImageProcess.ImageConverter(request.imageFile);

            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterAsync(request, origin));
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string token)
        {
            return Ok(await _accountService.ConfirmAccountAsync(userId, token));
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgorPasswordAsync(ForgotPassword request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.ForgotPasswordAsync(request, origin));
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPasswordAsync(ResetPassword request)
        {
            return Ok(await _accountService.ResetPasswordAsync(request));
        }

    }
}
