using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxU_Backend.Core.Application.DTOS.Publication
{
    public class UpdatePublicationRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe de Insertar una Descripcion"), DataType(DataType.Text)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Debe de Insertar una Descripcion"), DataType(DataType.Text)]
        public byte[]? ImageUrl { get; set; }
        public DateTime Created_At { get; set; }
        public string? UserId { get; set; }
        public byte[]? userPicture { get; set; }
        public string? userName { get; set; }
    }
}
