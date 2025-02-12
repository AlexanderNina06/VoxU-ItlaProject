using AutoMapper;
using Microsoft.AspNetCore.Http;
using VoxU_Backend.Core.Application.DTOS.Account;
using VoxU_Backend.Core.Application.DTOS.Publication;
using VoxU_Backend.Core.Application.Interfaces.Repositories;
using VoxU_Backend.Core.Application.Interfaces.Services;
using VoxU_Backend.Core.Domain.Entities;

namespace VoxU_Backend.Core.Application.Services
{
    public class PublicationService : GenericService<GetPublicationResponse, SavePublicationRequest, Publications>, IPublicationService
    {
        private readonly IMapper _mapper;
        private readonly IPublicationRepository _publicationRepository;
        private readonly ICommentsRepository _commentsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse UserSession;

        public PublicationService(IMapper mapper, IPublicationRepository publicationRepository, IHttpContextAccessor httpContextAccessor, ICommentsRepository commentsRepository) : base(mapper, publicationRepository)
        {
            _mapper = mapper;
            _publicationRepository = publicationRepository;
            _httpContextAccessor = httpContextAccessor;
            //_accountService = accountService;
            _commentsRepository = commentsRepository;
            //_friendsRepository = friendsRepository;
        }

        public Task<List<GetPublicationResponse>> GetFriendsPublicationsWithInclude()
        {
            throw new NotImplementedException();
        }

        public Task<List<GetPublicationResponse>> GetPublicationsWithInclude()
        {
            throw new NotImplementedException();
        }
    }
}
