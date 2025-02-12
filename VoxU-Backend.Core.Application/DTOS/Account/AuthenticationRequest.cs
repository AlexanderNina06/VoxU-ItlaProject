using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxU_Backend.Core.Application.DTOS.Account
{
    public class AuthenticationRequest
    {
        public string CollegeId { get; set; }
        public string Password { get; set; }
    }
}
