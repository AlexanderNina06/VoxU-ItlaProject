using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxU_Backend.Core.Application.DTOS.Account
{
    public class lockUnlockUserResponse
    {
        public string? error { get; set; }
        public bool hasError { get; set; }

        public string Result { get; set; }
    }
}
