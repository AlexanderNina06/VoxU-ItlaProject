using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxU_Backend.Core.Application.DTOS.Report
{
    public class GetReportResponse
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? Created_At { get; set; }
        public int PublicationId { get; set; }
        public string UserId { get; set; }
    }
}
