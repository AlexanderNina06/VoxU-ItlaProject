using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VoxU_Backend.Core.Application.DTOS.Comments;
using VoxU_Backend.Core.Application.DTOS.Publication;
using VoxU_Backend.Core.Application.Interfaces.Services;
using VoxU_Backend.Core.Application.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VoxU_Backend.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentsService;
        private readonly IAccountService _accountService;
        public CommentsController(ICommentService commentsService, IAccountService accountService)
        {
            _commentsService = commentsService;
            _accountService = accountService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Obtener todas las publicaciones",
            Description = "Recupera una lista de todas las publicaciones en el sistema.."
        )]
        [HttpGet()]
        public async Task<IActionResult> Get()
        {

            var comments = await _commentsService.GetAllVm();

            if (comments is null)
            {
                return NotFound();
            }

            return Ok(comments);

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
                var comment = await _commentsService.GetVmById(id);

                if (comment is null)
                {
                    return NotFound();
                }

                return Ok(comment);

            }
            catch (Exception ex)
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
        public async Task<IActionResult> Post([FromBody] SaveCommentsRequest saveCommentsRequest)
        {

            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(saveCommentsRequest);
                }

                saveCommentsRequest.CommentUserPicture = await _accountService.FindImageUserId(saveCommentsRequest.UserId);

                await _commentsService.AddAsyncVm(saveCommentsRequest);
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
        public async Task<IActionResult> Put(SaveCommentsRequest request, int Id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(request);
                }

                await _commentsService.UpdateAsyncVm(request, Id);
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

                await _commentsService.DeleteVmAsync(id);
                return StatusCode(StatusCodes.Status204NoContent);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }


        }
    }
}
