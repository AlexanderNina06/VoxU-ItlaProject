using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Domain.Entities;

namespace VoxU_Backend.Core.Application.DTOS.Category
{
    public class GetCategoryResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
