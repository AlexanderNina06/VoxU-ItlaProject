using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VoxU_Backend.Core.Application.DTOS.Book;
using VoxU_Backend.Core.Application.DTOS.Library;
using VoxU_Backend.Core.Application.Interfaces.Repositories;
using VoxU_Backend.Core.Application.Interfaces.Services;
using VoxU_Backend.Core.Application.Services;
using VoxU_Backend.Core.Domain.Entities;
using VoxU_Backend.Persistence.Shared.Service;

namespace VoxU_Backend.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IBookRepository _documentRepository;

        public BookController(ICloudinaryService cloudinaryService, IBookRepository documentRepository)
        {
            _cloudinaryService = cloudinaryService;
            _documentRepository = documentRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UploadDocument([FromForm] SaveFileRequest request)
        {
            if (request.file == null || request.file.Length == 0)
                return BadRequest("El archivo es requerido.");

            var url = await _cloudinaryService.UploadPdfAsync(request.file);

            var document = new Book { Name = request.file.FileName, Url = url };
            await _documentRepository.AddAsync(document);

            return Ok(new { message = "Documento subido con éxito", document.Url });
        }

        [HttpGet]
        public async Task<IActionResult> GetDocuments()
        {
            var documents = await _documentRepository.GetAllAsync();
            return Ok(documents);
        }
    }
}
