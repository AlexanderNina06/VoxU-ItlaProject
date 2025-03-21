using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxU_Backend.Core.Application.DTOS.SellPublication
{
    public class SaveSellPublication
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe de Insertar una Descripcion"), DataType(DataType.Text)]
        public string? Description { get; set; }
        public byte[]? ImageUrl { get; set; }
        public string Name { get; set; }
        public IFormFile imageFile { get; set; }
        public DateTime Created_At => DateTime.Now;
        public string? UserId { get; set; }
        public byte[]? userPicture { get; set; }
        public string? userName { get; set; }
        public double? Price { get; set; }
        public string? Place { get; set; }
        public int CategoryId { get; set; }
    }
}
