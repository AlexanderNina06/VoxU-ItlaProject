using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.DTOS.Comments;
using VoxU_Backend.Core.Application.DTOS.Report;

namespace VoxU_Backend.Core.Application.DTOS.Publication
{
    public class GetPublicationResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public byte[]? ImageUrl { get; set; }
        public object? ImageFront { get; set; }
        public DateTime Created_At { get; set; }
        public string UserId { get; set; }
        public byte[] userPicture { get; set; }
        public string userName { get; set; }
        public bool? isBlocked { get; set; }
        public int CommentsCount { get; set; }
        public ICollection<GetCommentsResponse>? Comments { get; set; }
        public ICollection<GetReportResponse>? Reports { get; set; }
    }
}
