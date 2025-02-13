using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.DTOS.Account;
using VoxU_Backend.Core.Application.DTOS.Replies;
using VoxU_Backend.Core.Application.Interfaces.Repositories;
using VoxU_Backend.Core.Application.Interfaces.Services;
using VoxU_Backend.Core.Domain.Entities;

namespace VoxU_Backend.Core.Application.Services
{
    public class RepliesService : GenericService<GetRepliesReponse, SaveRepliesRequest, Replies>, IRepliesService
    {
        private readonly IRepliesRepository _repliesRepository;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse userSession;

        public RepliesService(IRepliesRepository repliesRepository, IMapper mapper) : base(mapper, repliesRepository)
        {
            _repliesRepository = repliesRepository;
            _mapper = mapper;
        }

    }
}
