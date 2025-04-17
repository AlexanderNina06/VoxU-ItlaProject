using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;
using Swashbuckle.AspNetCore.Annotations;
using VoxU_Backend.Core.Application.DTOS.Comments;
using VoxU_Backend.Core.Application.DTOS.Publication;
using VoxU_Backend.Core.Application.DTOS.Replies;
using VoxU_Backend.Core.Application.DTOS.SellPublication;
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
        private readonly IMapper _mapper;
        private readonly ISellpublicationService _sellPublicationService;
        public SellPublicationController(ISellpublicationService sellPublicationService, IMapper mapper)
        {
            _mapper = mapper;
            _sellPublicationService = sellPublicationService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Buscar SellPublications (Name)",
            Description = "Recupera una lista de todas las SellPublicaciones buscadas por nombre"
        )]
        [HttpGet]
        public async Task<IActionResult> GetByName(string Name, int? CategoryId)
        {

            var publications = await _sellPublicationService.GetSellPublicationsByName(Name, CategoryId);


            if (publications is null)
            {
                return NotFound();
            }

            return Ok(publications);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
         Summary = "Publicaciones de ventas",
         Description = "Retorna todas las publicaciones de ventas"
         )]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetSellPublications()
        {
            try
            {
                var Publications = await _sellPublicationService.GetAllVm();
                return Ok(Publications);
           
            } catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Crear publicaciones de ventas",
            Description = "Crea una nueva publicación de ventas en el sistema."
        )]
        [HttpPost]
        public async Task<IActionResult> Post(SaveSellPublication saveSellPublicationRequest)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(saveSellPublicationRequest);
                }

                byte[] ImageBytes = ImageProcess.ImageConverter(saveSellPublicationRequest.imageFile);
                saveSellPublicationRequest.ImageUrl = ImageBytes;
                saveSellPublicationRequest.userPicture = ImageBytes;

                await _sellPublicationService.AddAsyncVm(saveSellPublicationRequest);
                return Created();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }


        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
           Summary = "Actualizar publicacion de venta",
           Description = "Actualiza una publicación de venta existente en el sistema."
       )]
        [HttpPut]
        public async Task<IActionResult> Put(SaveSellPublication requestDto)
        {  
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(requestDto);
                }
                //var request = _mapper.Map<SaveSellPublication>(requestDto);
                byte[] ImageBytes = ImageProcess.ImageConverter(requestDto.imageFile);
                requestDto.ImageUrl = ImageBytes;

                await _sellPublicationService.Update(requestDto);
                return Ok(requestDto);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

