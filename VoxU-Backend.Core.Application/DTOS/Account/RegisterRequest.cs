using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxU_Backend.Core.Application.DTOS.Account
{
    public class RegisterRequest
    {
        public string? Id { get; set; }
        public string? collegeId { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? User { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTime? Created_At { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
