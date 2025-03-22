using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxU_Backend.Core.Application.DTOS.SellPublication
{
    public class RequestSaveSellPublication
    {
        [Required(ErrorMessage = "Debe de Insertar una Descripcion"), DataType(DataType.Text)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Debe de Insertar un nombre"), DataType(DataType.Text)]
        public string Name { get; set; }
        public IFormFile imageFile { get; set; }

        [Required(ErrorMessage = "Debe de Insertar un precio"), DataType(DataType.Text)]
        public double? Price { get; set; }

        [Required(ErrorMessage = "Debe de Insertar un lugar"), DataType(DataType.Text)]
        public string? Place { get; set; }

        [Required(ErrorMessage = "Debe de Insertar una categoria"), DataType(DataType.Text)]
        public int CategoryId { get; set; }
    }
}
