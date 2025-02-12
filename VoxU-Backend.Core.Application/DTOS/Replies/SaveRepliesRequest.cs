using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxU_Backend.Core.Application.DTOS.Replies
{
    public class SaveRepliesRequest
    {
        [Required(ErrorMessage = "Debe insertar una respuesta"), DataType(DataType.Text)]
        public string Reply { get; set; }
        public string UserId { get; set; }
        public string ReplyUserPicture { get; set; }
        public string ReplyUserName { get; set; }
        public int CommentId { get; set; }
    }
}
