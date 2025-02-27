using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxU_Backend.Core.Application.DTOS.Account
{
    public class RegisterRequest
    {
        public string? collegeId { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? User { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public IFormFile imageFile { get; set; }
        public DateTime? Created_At => DateTime.Now;
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
