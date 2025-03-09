using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxU_Backend.Core.Application.DTOS.Account
{
    public class UpdateAccountResponse
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? User { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public string PhoneNumber { get; set; }
        public string? Error { get; set; }
        public bool HasError {    get; set; }

    }
}
