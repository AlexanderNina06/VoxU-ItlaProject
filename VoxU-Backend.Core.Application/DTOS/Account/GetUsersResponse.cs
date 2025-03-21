using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxU_Backend.Core.Application.DTOS.Account
{
    public class GetUsersResponse
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? User { get; set; }
        public bool IsBlocked { get; set; }
        public string? Description { get; set; }
        public string? Career { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public DateTime? Created_At { get; set; }
    }
}
