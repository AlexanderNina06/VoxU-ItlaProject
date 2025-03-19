using MessagePack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using VoxU_Backend.Core.Application.DTOS.Publication;
using VoxU_Backend.Core.Application.DTOS.Report;
using VoxU_Backend.Core.Application.Interfaces.Services;
using VoxU_Backend.Core.Application.Services;
using VoxU_Backend.Helpers;

namespace VoxU_Backend.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IPublicationService _publicationService;
        public ReportController(IReportService reportService, IPublicationService publicationService)
        {
            _reportService = reportService;
            _publicationService = publicationService;
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Post(SaveReportRequest saveReportRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(saveReportRequest);
                }

                var report = await _reportService.AddAsyncVm(saveReportRequest);
              
                if (report == null)
                {
                    return BadRequest("Ya creaste un reporte a esta publicacion");
                }

                return Created();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetReportByPublicationId(int publicationId)
        {
            try
            {
              
               var report = await _reportService.getReportByPublicationId(publicationId);

                if(report == null)
                {
                    return NotFound();
                }

                return Ok(report);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        [HttpGet("ReportPublications")]
        public async Task<IActionResult> getReportPublications()
        {
            try
            {

                var publications = await _publicationService.GetPublicationsWithReports();

                if (publications == null)
                {
                    return NotFound();
                }

                return Ok(publications);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }



        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
             
                await _reportService.DeleteVmAsync(Id);

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

    }
}
