using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.DTOS.SellPublication;
using VoxU_Backend.Core.Application.DTOS;
using VoxU_Backend.Core.Domain.Entities;
using VoxU_Backend.Core.Application.DTOS.Report;
using VoxU_Backend.Core.Application.Services;

namespace VoxU_Backend.Core.Application.Interfaces.Services
{
    public interface IReportService : IGenericService<GetReportResponse, SaveReportRequest, Report>
    {
        Task<List<GetReportResponse>> getReportByPublicationId(int publicationId);

    }
}
