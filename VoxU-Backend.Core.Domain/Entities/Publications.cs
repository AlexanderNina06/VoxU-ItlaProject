using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxU_Backend.Core.Domain.Entities
{
    public class Publications
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public byte[]? ImageUrl { get; set; }
        public DateTime? Created_At { get; set; }
        public string? UserId { get; set; }
        public byte[]? userPicture { get; set; }
        public string? userName { get; set; }
        public ICollection<Comments>? Comments {get; set;}
        public ICollection<Report>? Reports {get; set;}

    }
}
