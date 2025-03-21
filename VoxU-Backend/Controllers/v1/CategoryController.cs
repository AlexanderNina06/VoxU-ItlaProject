using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VoxU_Backend.Core.Application.DTOS.Category;
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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Obtener todas las categoria",
            Description = "Recupera una lista de todas las categoria en el sistema.."
        )]
        [HttpGet()]
        public async Task<IActionResult> Get()
        {

            var comments = await _categoryService.GetAllVm();

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
            Summary = "Obtener categoria por el Id",
            Description = "Recupera una categoria específica por su ID."
        )]
        [HttpGet("GetById")]
        public async Task<IActionResult> GetCategoryById([FromQuery] int id)
        {

            try
            {
                var comment = await _categoryService.GetVmById(id);

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
            Summary = "Crear categoria",
            Description = "Crea una nueva categoria."
        )]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SaveCategoryRequest saveCategoryRequest)
        {

            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(saveCategoryRequest);
                }

                await _categoryService.AddAsyncVm(saveCategoryRequest);
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
            Summary = "Actualizar categoria",
            Description = "Actualiza una categoria existente en el sistema."
        )]
        [HttpPut]
        public async Task<IActionResult> Put(SaveCategoryRequest request)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(request);
                }

                await _categoryService.Update(request);
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
            Summary = "Eliminar categoria",
            Description = "Elimina una categoria del sistema."
        )]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {

                await _categoryService.DeleteVmAsync(id);
                return StatusCode(StatusCodes.Status204NoContent);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }


        }
    }
}
