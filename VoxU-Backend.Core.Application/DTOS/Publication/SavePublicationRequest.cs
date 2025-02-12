using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace VoxU_Backend.Core.Application.DTOS.Publication
{
    public class SavePublicationRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe de Insertar una Descripcion"), DataType(DataType.Text)]
        public string Description { get; set; }

        public string? ImageUrl { get; set; }
        public IFormFile? ImageFile { get; set; }
        public DateTime Created_At { get; set; }
        public string? UserId { get; set; }
        public string? userPicture { get; set; }
        public string? userName { get; set; }
    }
}
