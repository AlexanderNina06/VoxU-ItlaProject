using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.DTOS.Account;
using VoxU_Backend.Core.Application.DTOS.Publication;
using VoxU_Backend.Core.Domain.Entities;

namespace VoxU_Backend.Core.Application.Interfaces.Services
{
    public interface IPublicationService : IGenericService<GetPublicationResponse, SavePublicationRequest, Publications>
    {
        Task<SavePublicationRequest> AddAsyncVm(SavePublicationRequest saveViewModel);
        Task<List<GetPublicationResponse>> GetPublicationsWithInclude();
        Task<List<GetPublicationResponse>> GetPublicationsByCareerWithInclude(List<GetUsersResponse> userlist, string carrera);

    }
}
