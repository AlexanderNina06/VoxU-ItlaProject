using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxU_Backend.Core.Domain.Entities
{
    public class Replies
    {
        public int Id { get; set; }
        public string? Reply { get; set; }
        public string? UserId { get; set; }
        public byte[]? ReplyUserPicture { get; set; }
        public string? ReplyUserName { get; set; }
        public int CommentId { get; set; }
        public Comments? Comments { get; set; }
    }
}
