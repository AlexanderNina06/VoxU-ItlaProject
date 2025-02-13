using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.DTOS.Comments;
using VoxU_Backend.Core.Application.DTOS.Replies;
using VoxU_Backend.Core.Application.Interfaces.Repositories;
using VoxU_Backend.Core.Application.Interfaces.Services;
using VoxU_Backend.Core.Domain.Entities;

namespace VoxU_Backend.Core.Application.Services
{
    public class CommentsService : GenericService<GetCommentsResponse, SaveCommentsRequest, Comments>, ICommentService
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IMapper _mapper;
       public CommentsService(ICommentsRepository commentsRepository, IMapper mapper) : base(mapper, commentsRepository)
       {
            _commentsRepository = commentsRepository;
            _mapper = mapper;
       }

        public async Task<List<GetCommentsResponse>> GetCommentsWithInclude()
        {
            var commentsList = await _commentsRepository.GetAllWithInclude(new List<string> { "replies" });

            return commentsList.Select(comment => new GetCommentsResponse
            {
                Id = comment.Id,
                IdPublication = comment.IdPublication,
                replies = comment.replies.Select(reply => new GetRepliesReponse
                {
                    Reply = reply.Reply,
                    UserId = reply.UserId,
                    ReplyUserName = reply.ReplyUserName,
                    ReplyUserPicture = reply.ReplyUserPicture

                }).ToList()

            }).ToList();

        }
    }
}
