using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxU_Backend.Core.Application.DTOS.Report
{
    public class SaveReportRequest
    {
        public string Tipo { get; set; }
        public string? Descripcion { get; set; }
        public int PublicationId { get; set; }
        public string UserId { get; set; }
        public DateTime Created_At => DateTime.Now;
    }
}
