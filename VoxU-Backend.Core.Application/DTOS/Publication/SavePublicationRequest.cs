using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;


namespace VoxU_Backend.Core.Application.DTOS.Publication
{
    public class SavePublicationRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe de Insertar una Descripcion"), DataType(DataType.Text)]
        public string Description { get; set; }

        public byte[]? ImageUrl { get; set; }

        public IFormFile? imageFile { get; set; }   
        public DateTime  Created_At => DateTime.Now;
        public string? UserId { get; set; }
        public byte[]? userPicture { get; set; }
        public string? userName { get; set; }
        public bool? isBlocked { get; set; } = false;
    }
}
