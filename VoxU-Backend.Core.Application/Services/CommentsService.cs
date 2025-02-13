using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.DTOS.Comments;
using VoxU_Backend.Core.Application.Interfaces.Repositories;
using VoxU_Backend.Core.Application.Interfaces.Services;
using VoxU_Backend.Core.Domain.Entities;

namespace VoxU_Backend.Core.Application.Services
{
    public class CommentsService : GenericService<GetCommentsResponse, SaveCommentsRequest, Comments>, ICommentsService
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IMapper _mapper;
       public CommentsService(ICommentsRepository commentsRepository, IMapper mapper) : base(mapper, commentsRepository)
       {
            _commentsRepository = commentsRepository;
            _mapper = mapper;
       }


    }
}
