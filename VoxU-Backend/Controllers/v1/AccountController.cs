using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Swashbuckle.AspNetCore.Annotations;
using VoxU_Backend.Core.Application.DTOS.Account;
using VoxU_Backend.Core.Application.DTOS.Account.Delete;
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
        [SwaggerOperation(
            Summary = "Autenticar",
            Description = "Permite a un usuario autenticarse en el sistema y obtener un token de acceso."
        )]
        [HttpPost("authenticate")] //Luego de definir el tipo de verbo, podemos customizar la ruta hacia el endpoint como lo vemos aqui 
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            var user = await _accountService.AuthenticateAsync(request);
           
            if(user.HasError)
            {
                return Ok(user);
            } else
            {
                return BadRequest(user.Error);
            }

        }

        [SwaggerOperation(
            Summary = "Registrar usuarios",
            Description = "Permite registrar un nuevo usuario en el sistema con sus datos de acceso."
        )]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromForm]RegisterRequest request)
        {
            //Converting Image to bytes
            request.ProfilePicture = ImageProcess.ImageConverter(request.imageFile);

            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterAsync(request, origin));
        }

        [SwaggerOperation(
            Summary = "Confirmar Email",
            Description = "Verifica y confirma la dirección de correo electrónico de un usuario en el sistema."
        )]
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string token)
        {
            return Ok(await _accountService.ConfirmAccountAsync(userId, token));
        }

        [SwaggerOperation(
            Summary = "Recuperar contraseña",
            Description = "Envía un enlace o código de recuperación al correo electrónico del usuario para restablecer su contraseña."
        )]
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgorPasswordAsync(ForgotPassword request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.ForgotPasswordAsync(request, origin));
        }

        [SwaggerOperation(
            Summary = "Restablecer contraseña",
            Description = "Permite a un usuario establecer una nueva contraseña utilizando un token de recuperación."
        )]
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPasswordAsync(ResetPassword request)
        {
            return Ok(await _accountService.ResetPasswordAsync(request));
        }

        [HttpPut("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile(UpdateAccountRequest request)
        {
            return Ok(await _accountService.UpdateUser(request));
        }  
        
        [HttpDelete("DeleteProfile")]
        public async Task<IActionResult> DeleteProfile(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return BadRequest();

            var response = await _accountService.DeleteUser(userName);

            if (response.hasError) return StatusCode(StatusCodes.Status500InternalServerError, response.error);

            return Ok();
        }  
        
        [HttpPut("CastProfile")]
        public async Task<IActionResult> CastProfile(string userName)
        {
            return Ok(await _accountService.lockUnlockUser(userName));
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveUsers()
        {
            return Ok(await _accountService.GetActiveUsers());
        }
    }
}
