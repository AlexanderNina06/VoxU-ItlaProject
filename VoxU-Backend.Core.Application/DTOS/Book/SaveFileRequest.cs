using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxU_Backend.Core.Application.DTOS.Book
{
    public class SaveFileRequest
    {
        public IFormFile File { get; set; }
        public IFormFile? CoverImage { get; set; }
    }
}
