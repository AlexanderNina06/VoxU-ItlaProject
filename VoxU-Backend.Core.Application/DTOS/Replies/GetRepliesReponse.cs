using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.DTOS.Comments;

namespace VoxU_Backend.Core.Application.DTOS.Replies
{
    public class GetRepliesReponse
    {
        public int Id { get; set; }
        public string Reply { get; set; }
        public string UserId { get; set; }
        public byte[] ReplyUserPicture { get; set; }
        public string ReplyUserName { get; set; }
        public int CommentId { get; set; }
        public GetCommentsResponse? Comments { get; set; }
    }
}
