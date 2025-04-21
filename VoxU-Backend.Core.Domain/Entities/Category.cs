using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxU_Backend.Core.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Icon { get; set; }
        public ICollection<SellPublications> sellPublications { get; set; }
    }
}
