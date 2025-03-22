using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VoxU_Backend.Core.Application.DTOS.Library;
using VoxU_Backend.Core.Application.Interfaces.Services;
using VoxU_Backend.Core.Application.Services;

namespace VoxU_Backend.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(SaveBookRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("No se pudo obtener el UserId del token.");
            }

            request.UserId = userId;

            // Aquí, no necesitas establecer el Id ya que EF lo generará automáticamente
            var bookCreated = await _bookService.AddAsyncVm(request);
            if (bookCreated == null)
            {
                return StatusCode(500, "Error al crear el libro.");
            }

            // Si hay un archivo PDF, subirlo y actualizar el libro
            if (request.PdfFile != null)
            {
                // Ahora bookCreated.Id tiene el Id generado por la base de datos
                var bookUrl = UploadFile(request.PdfFile, bookCreated.Id);
                bookCreated.FilePath = bookUrl;
                await _bookService.Update(bookCreated);
            }

            return CreatedAtAction(nameof(CreateBook), new { id = bookCreated.Id }, bookCreated);
        }

        private string UploadFile(IFormFile file, int Id, bool editMode = false, string imageUrl = "")
        {
            if (editMode)
            {
                if (file == null)
                {
                    // Si no hay un archivo y editMode es true, retorname la misma imageUrl
                    return imageUrl;
                }

            }

            string basePath = $"/image/PublicationsImages/{Id}"; //Ruta inicial
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //if folder doesn't exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Formando el nombre de la imagen

            Guid guid = Guid.NewGuid(); // Genero un guid que es un string generado unico
            FileInfo fileInfo = new(file.FileName); // recupero la info de la imagen
            string fileName = guid + fileInfo.Extension;

            string fileWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileWithPath, FileMode.Create))
            {
                file.CopyTo(stream); // copiamos nuestro archivo original y se lo pegamos a la ruta que ya creamos
            }

            if (editMode)
            {
                //Cuando editamos y agregamos un nuevo archivo, deseamos eliminar el anterior 
                //Debemos de recuperar el path del anterior e eliminarlo
                string[] oldImagePart = imageUrl.Split("/");
                string oldImageName = oldImagePart[^1]; //En la ultima posicion esta el name del archivo, lo recuperamos
                string completeImageOldPath = Path.Combine(path, oldImageName);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }

            }


            //Retornamos la ruta de la imagen
            return $"{basePath}/{fileName}";
        }
    }
}
