using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace VoxU_Backend.Core.Application.DTOS.Publication
{
    public class SavePublicationRequest
    {

        [Required(ErrorMessage = "Debe de Insertar una Descripcion"), DataType(DataType.Text)]
        public string Description { get; set; }

        public byte[]? ImageUrl { get; set; }

        public IFormFile imageFile { get; set; }   
        public DateTime  Created_At => DateTime.Now;
        public string? UserId { get; set; }
        public byte[]? userPicture { get; set; }
        public string? userName { get; set; }
    }
}
