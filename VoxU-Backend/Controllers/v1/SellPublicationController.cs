using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;
using Swashbuckle.AspNetCore.Annotations;
using VoxU_Backend.Core.Application.DTOS.Comments;
using VoxU_Backend.Core.Application.DTOS.Publication;
using VoxU_Backend.Core.Application.DTOS.Replies;
using VoxU_Backend.Core.Application.Interfaces.Services;
using VoxU_Backend.Core.Application.Services;
using VoxU_Backend.Core.Domain.Entities;
using VoxU_Backend.Helpers;
using VoxU_Backend.Pesistence.Identity.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VoxU_Backend.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class SellPublicationController : ControllerBase
    {
        private readonly ISellpublicationService _sellPublicationService;
        public SellPublicationController(ISellpublicationService sellPublicationService)
        {
            _sellPublicationService = sellPublicationService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Buscar SellPublications (Name)",
            Description = "Recupera una lista de todas las SellPublicaciones buscadas por nombre"
        )]
        [HttpGet]
        public async Task<IActionResult> GetByName(string Name)
        {

            var publications = await _sellPublicationService.GetSellPublicationsByName(Name);


            if (publications is null)
            {
                return NotFound();
            }

            return Ok(publications);

        }
    }

}

