﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxU_Backend.Core.Application.DTOS.Email
{
    public class EmailRequest
    {
        public string? To { get; set; }
        public string? Body { get; set; }
        public string? Subject { get; set; }
    }
}
