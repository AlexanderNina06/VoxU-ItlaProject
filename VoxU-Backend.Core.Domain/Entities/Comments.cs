using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxU_Backend.Core.Domain.Entities
{
    public class Comments
    {
        public int Id { get; set; }
        public string? Comment { get; set; }
        public string? UserId { get; set; }
        public string? CommentUserPicture { get; set; }
        public string? CommentUserName { get; set; }
        public int? IdPublication { get; set; }
        public Publications? Publications { get; set; }
        public ICollection<Replies>? replies { get; set; }

    }
}
