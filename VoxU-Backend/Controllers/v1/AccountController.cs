using Microsoft.AspNetCore.Mvc;

namespace VoxU_Backend.Controllers.v1
{
    [ApiController()]
    public class AccountController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
