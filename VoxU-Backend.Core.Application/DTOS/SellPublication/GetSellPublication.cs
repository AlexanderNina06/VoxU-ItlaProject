using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Domain.Entities;

namespace VoxU_Backend.Core.Application.DTOS
{
    public class GetSellPublication
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public byte[]? ImageUrl { get; set; }
        public DateTime? Created_At { get; set; }
        public string? userName { get; set; }
        public double? Price { get; set; }
        public string? Place { get; set; }
        public int CategoryId { get; set; }
    }
}
