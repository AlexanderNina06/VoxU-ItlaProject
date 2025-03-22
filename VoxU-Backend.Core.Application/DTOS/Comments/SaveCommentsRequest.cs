using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.DTOS.Publication;

namespace VoxU_Backend.Core.Application.DTOS.Comments
{
    public class SaveCommentsRequest
    { 
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe de insertar un comentario !"), DataType(DataType.Text)]
        public string Comment { get; set; }
        public string UserId { get; set; }
        public byte[]? CommentUserPicture { get; set; } //Recuperar utilizando el usuario logeado
        public string CommentUserName { get; set; }
        public int IdPublication { get; set; }
    }
}
