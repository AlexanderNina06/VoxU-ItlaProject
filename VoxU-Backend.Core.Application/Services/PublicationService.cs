﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using VoxU_Backend.Core.Application.DTOS.Account;
using VoxU_Backend.Core.Application.DTOS.Comments;
using VoxU_Backend.Core.Application.DTOS.Publication;
using VoxU_Backend.Core.Application.DTOS.Replies;
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

        public PublicationService(IMapper mapper, IPublicationRepository publicationRepository, ICommentsRepository commentsRepository) : base(mapper, publicationRepository)
        {
            _mapper = mapper;
            _publicationRepository = publicationRepository;
            _commentsRepository = commentsRepository;

        }

        

        public Task<List<GetPublicationResponse>> GetFriendsPublicationsWithInclude()
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetPublicationResponse>> GetPublicationsWithInclude()
        {
            var publicationsList = await _publicationRepository.GetAllWithInclude(new List<string> { "Comments" });
            var commentsWithReplies = await _commentsRepository.GetAllWithInclude(new List<string> { "replies" });

            var tasks = publicationsList.Select(async publication =>
            {
                var commentsTasks = publication.Comments.Select(async comment =>
                {
                    //var userPicture = await _accountService.FindImageUserId(comment.UserId); // Asegúrate de que este método sea asincrónico
                    //var userName = await _accountService.FindUserNameById(comment.UserId); // Asegúrate de que este método sea asincrónico

                    return new GetCommentsResponse
                    {
                        Id = comment.Id,
                        Comment = comment.Comment,
                        UserId = comment.UserId,
                        CommentUserName = comment.CommentUserName,
                        CommentUserPicture = comment.CommentUserPicture,
                        replies = comment.replies.Select(r => new GetRepliesReponse
                        {
                            Reply = r.Reply,
                            ReplyUserName = r.ReplyUserName,
                            ReplyUserPicture = r.ReplyUserPicture
                        }).ToList()

                    };
                });

                var comments = await Task.WhenAll(commentsTasks);

                return new GetPublicationResponse
                {
                    Id = publication.Id,
                    UserId = publication.UserId,
                    Description = publication.Description,
                    ImageUrl = publication.ImageUrl,
                    Created_At = (DateTime)publication.Created_At,
                    userPicture = publication.userPicture,
                    userName = publication.userName,
                    Comments = comments.ToList(),
                    CommentsCount = comments.Length

                };

            });

             var publications = await Task.WhenAll(tasks);
                return publications.ToList();

        }
    }
}
