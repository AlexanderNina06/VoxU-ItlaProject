using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;
using VoxU_Backend.Core.Application.DTOS.Publication;
using VoxU_Backend.Core.Application.Interfaces.Services;
using VoxU_Backend.Core.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VoxU_Backend.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationsController : ControllerBase
    {
      private readonly IPublicationService _publicationService;
      public PublicationsController(IPublicationService publicationService) 
      {
        _publicationService = publicationService;
        
      }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
           
            var publications = await _publicationService.GetAllVm();

            if (publications is null)
            {
                return NotFound();
            }

            return Ok(publications);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublicationById([FromQuery]int id)
        {

            try
            {
                var publication = await _publicationService.GetVmById(id);

                if (publication is null)
                {
                    return NotFound();
                }

                return Ok(publication);

            } catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SavePublicationRequest savePublicationRequest)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(savePublicationRequest);
                }

                await _publicationService.AddAsyncVm(savePublicationRequest);
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
        [HttpPut]
        public async Task<IActionResult> Put(SavePublicationRequest request)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(request);
                }

                await _publicationService.UpdateAsyncVm(request, request.Id);
                return Ok(request);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }


        }
       

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
            
                await _publicationService.DeleteVmAsync(id);
                return StatusCode(StatusCodes.Status204NoContent);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }


        }

    }

  
 }

