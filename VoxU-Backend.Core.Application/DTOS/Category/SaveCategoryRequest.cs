using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxU_Backend.Core.Application.DTOS.Category
{
    public class SaveCategoryRequest
    {
        public string Nombre { get; set; }
        public string? Icon { get; set; }
        public IFormFile? IconFile { get; set; }
    }
}
