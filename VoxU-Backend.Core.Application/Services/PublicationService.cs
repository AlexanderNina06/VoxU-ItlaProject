using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;
using VoxU_Backend.Core.Application.DTOS.Account;
using VoxU_Backend.Core.Application.DTOS.Comments;
using VoxU_Backend.Core.Application.DTOS.Publication;
using VoxU_Backend.Core.Application.DTOS.Replies;
using VoxU_Backend.Core.Application.DTOS.Report;
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
        private readonly IAccountService _accountService;


        public PublicationService(IMapper mapper, IPublicationRepository publicationRepository, ICommentsRepository commentsRepository, IAccountService accountService) : base(mapper, publicationRepository)
        {
            _mapper = mapper;
            _publicationRepository = publicationRepository;
            _commentsRepository = commentsRepository;
            _accountService = accountService;
        }

        public async Task<List<GetPublicationResponse>> GetPublicationsByCareerWithInclude(List<GetUsersResponse> userlist, string carrera)
        {
            // Filter users by the specified career
            var usersInCareer = userlist.Where(user => user.Career == carrera).Select(user => user.Id).ToList();

            // Get all publications with comments and reports
            var publicationsList = await _publicationRepository.GetAllWithInclude(new List<string> { "Comments", "Reports" });

            //filter only no blocked publications
            publicationsList.Where(p => p.isBlocked == false);

            // Filter publications by users in the specified career
            var filteredPublications = publicationsList.Where(publication => usersInCareer.Contains(publication.UserId)).ToList();

            var commentsWithReplies = await _commentsRepository.GetAllWithInclude(new List<string> { "replies" });

            var tasks = filteredPublications.Select(async publication =>
            {
                var commentsTasks = publication.Comments.Select(async comment =>
                {
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
                    isBlocked = publication.isBlocked,
                    Comments = comments.ToList(),
                    CommentsCount = comments.Length,
                    Reports = publication.Reports.Select(r => new GetReportResponse
                    {
                        Id = r.Id,
                        UserId = r.UserId,
                        PublicationId = r.PublicationId,
                        Descripcion = r.Descripcion
                    }).ToList()
                };
            });

            var publications = await Task.WhenAll(tasks);
            return publications.ToList();
        }

        public async Task<List<GetPublicationResponse>> GetPublicationsWithReports()
        {
            var publicationsList = await _publicationRepository.GetAllWithInclude(new List<string> { "Reports" });
            
           publicationsList.Where(p => p.Reports != null);
           return publicationsList.Select(publication => new GetPublicationResponse {
                    Id = publication.Id,
                    UserId = publication.UserId,
                    Description = publication.Description,
                    ImageUrl = publication.ImageUrl,
                    Created_At = (DateTime)publication.Created_At,
                    userPicture = publication.userPicture,
                    userName = publication.userName,
                    isBlocked = publication.isBlocked,
                    Reports = publication.Reports.Select(r => new GetReportResponse
                    {
                        Id = r.Id,
                        UserId = r.UserId,
                        PublicationId = r.PublicationId,
                        Descripcion = r.Descripcion
                    }).ToList()
            }).ToList();

        }


        public async Task<List<GetPublicationResponse>> GetPublicationsWithInclude()
        {
            var publicationsList = await _publicationRepository.GetAllWithInclude(new List<string> { "Comments", "Reports" });
            var commentsWithReplies = await _commentsRepository.GetAllWithInclude(new List<string> { "replies" });
            

            var tasks = publicationsList.Select(async publication =>
            {
                var commentsTasks = publication.Comments.Select(async comment =>
                {
                    var userPicture = await _accountService.FindImageUserId(comment.UserId);
                    var userName = await _accountService.FindUserNameById(comment.UserId);

                    return new GetCommentsResponse
                    {
                        Id = comment.Id,
                        Comment = comment.Comment,
                        UserId = comment.UserId,
                        CommentUserName = userName,
                        CommentUserPicture = userPicture,
                        replies = comment.replies.Select(r => new GetRepliesReponse
                        {
                            Reply = r.Reply,
                            ReplyUserName = r.ReplyUserName,
                            ReplyUserPicture = r.ReplyUserPicture
                        }).ToList()

                    };
                });

                var comments = await Task.WhenAll(commentsTasks);

                var publicationUserPictureBytes = await _accountService.FindImageUserId(publication.UserId);
                var publicationUserName = await _accountService.FindUserNameById(publication.UserId);

                return new GetPublicationResponse
                {
                    Id = publication.Id,
                    UserId = publication.UserId,
                    Description = publication.Description,
                    ImageUrl = publication.ImageUrl,
                    Created_At = (DateTime)publication.Created_At,
                    userPicture = publicationUserPictureBytes, 
                    userName = publicationUserName,
                    isBlocked = publication.isBlocked,
                    Comments = comments.ToList(),
                    CommentsCount = comments.Length,
                    Reports = publication.Reports.Select(r => new GetReportResponse
                    {
                        Id = r.Id,
                        UserId = r.UserId,
                        PublicationId = r.PublicationId,
                        Descripcion = r.Descripcion
                    }).ToList()

                };

            });

             var publications = await Task.WhenAll(tasks);
             return publications.ToList();

        }

        public async Task BlockPublication(int PublicationId)
        {
            var publication = await _publicationRepository.GetById(PublicationId);

            publication.isBlocked = true;

            await _publicationRepository.UpdateAsync(publication);

        }


    }
}
