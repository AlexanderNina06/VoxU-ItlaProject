using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxU_Backend.Core.Application.DTOS.Account
{
    public class AuthenticationResponse
    {
        public string? Id { get; set; }
        public string? collegeId { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? User { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public DateTime? Created_At { get; set; }
        public bool IsVerified { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? Error { get; set; }
        public bool HasError { get; set; }
    }
}
