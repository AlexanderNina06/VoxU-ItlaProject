using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.DTOS.Report;
using VoxU_Backend.Core.Application.Interfaces.Repositories;
using VoxU_Backend.Core.Application.Interfaces.Services;
using VoxU_Backend.Core.Domain.Entities;

namespace VoxU_Backend.Core.Application.Services
{
    public class ReportService : GenericService<GetReportResponse, SaveReportRequest, Report>, IReportService
    {
        private readonly IReportRepository _reportRepository;
        public ReportService(IMapper mapper, IReportRepository reportRepository) : base(mapper, reportRepository)
        {
            _reportRepository = reportRepository;

        }


        public async Task<List<GetReportResponse>> getReportByPublicationId(int publicationId)
        {
            var reports = await _reportRepository.GetAllAsync();

           return reports.Select(r => new GetReportResponse
           {
               Id = r.Id,
               Tipo = r.Tipo,
               Descripcion = r.Descripcion,
               PublicationId = r.PublicationId,
               Created_At = r.Created_At,
                UserId = r.UserId,
           }).ToList();

        }

        public override async Task<SaveReportRequest> AddAsyncVm(SaveReportRequest saveViewModel)
        {
            //Validar si el usuario ya tiene ha creado un reporte con ese id de publicacion

            var reports = await _reportRepository.GetAllAsync();

            var validate = reports.Where(r => r.UserId == saveViewModel.UserId && r.PublicationId == saveViewModel.PublicationId);

            if (validate.Count() >= 1)
            {
                return null;
            }

            SaveReportRequest reportAdded = await base.AddAsyncVm(saveViewModel);
            return reportAdded;
        }
    }
}
