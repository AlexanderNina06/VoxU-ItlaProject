using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VoxU_Backend.Core.Application.DTOS.Library
{
    public class SaveBookRequest
    {
        [JsonIgnore]
        public int Id { get; set; } 
        public string Title { get; set; }
        public string? UserId { get; set; }
        public IFormFile? PdfFile { get; set; }
        public string? FilePath { get; set; }
        public DateTime UploadDate => DateTime.Now;
    }
}
