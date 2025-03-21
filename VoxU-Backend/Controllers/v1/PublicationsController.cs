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
    public class PublicationsController : ControllerBase
    {
        private readonly IPublicationService _publicationService;
        private readonly IRepliesService _repliesService;
        private readonly IAccountService _accountService;
        public PublicationsController(IPublicationService publicationService, IRepliesService repliesService, IAccountService accountService)
        {
            _publicationService = publicationService;
            _repliesService = repliesService;
            _accountService = accountService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Obtener todas las publicaciones",
            Description = "Recupera una lista de todas las publicaciones en el sistema.."
        )]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
           
            var publications = await _publicationService.GetPublicationsWithInclude();


            if (publications is null)
            {
                return NotFound();
            }

            return Ok(getImageFront(publications));

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Obtener publicaciones por el Id",
            Description = "Recupera una publicación específica por su ID."
        )]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublicationById([FromQuery] int id)
        {

            try
            {
                var publication = await _publicationService.GetVmById(id);

                if (publication is null)
                {
                    return NotFound(); 
                }

                publication.ImageFront = File(publication.ImageUrl, "image/jpeg");

                return Ok(publication);

            } catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Crear publicaciones",
            Description = "Crea una nueva publicación en el sistema."
        )]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] SavePublicationRequest savePublicationRequest)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(savePublicationRequest);
                }

                byte[] ImageBytes = ImageProcess.ImageConverter(savePublicationRequest.imageFile);
                savePublicationRequest.ImageUrl = ImageBytes;
                savePublicationRequest.userPicture = ImageBytes;

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
        [SwaggerOperation(
            Summary = "Actualizar publicacion",
            Description = "Actualiza una publicación existente en el sistema."
        )]
        [HttpPut]
        public async Task<IActionResult> Put(SavePublicationRequest request)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(request);
                }
      
                await _publicationService.Update(request);
                return Ok(request);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }


        }
       

        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation(
            Summary = "Eliminar una publicacion",
            Description = "Elimina una publicación del sistema."
        )]
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


        private List<GetPublicationResponse> getImageFront(List<GetPublicationResponse> publications)
        {

            foreach (var publication in publications)
            {
                publication.ImageFront = File(publication.ImageUrl, "image/jpeg");

            }

            return publications;
        }

        /*public IActionResult AddReply(int Id)
        {
            SaveRepliesRequest saveRepliesView = new SaveRepliesRequest();
            saveRepliesView.CommentId = Id;
            return View(saveRepliesView);
        }*/

        [HttpPost("reply")]
        public async Task<IActionResult> AddReply([FromBody] SaveRepliesRequest saveReplyRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(saveReplyRequest);
            }

            saveReplyRequest.ReplyUserPicture = await _accountService.FindImageUserId(saveReplyRequest.UserId);

            await _repliesService.AddAsyncVm(saveReplyRequest);
            return Ok();
        }


    }

  
 }

