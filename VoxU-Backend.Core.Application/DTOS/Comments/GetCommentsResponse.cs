using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.DTOS.Publication;
using VoxU_Backend.Core.Application.DTOS.Replies;

namespace VoxU_Backend.Core.Application.DTOS.Comments
{
    public class GetCommentsResponse
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
        public string CommentUserPicture { get; set; }
        public string CommentUserName { get; set; }

        public int IdPublication { get; set; }
        public GetPublicationResponse? publications { get; set; }
        public ICollection<GetRepliesReponse>? replies { get; set; }
    }
}
