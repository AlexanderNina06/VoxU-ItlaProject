using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.DTOS.Comments;
using VoxU_Backend.Core.Application.DTOS.Publication;
using VoxU_Backend.Core.Application.DTOS.Replies;
using VoxU_Backend.Core.Application.DTOS.Report;
using VoxU_Backend.Core.Domain.Entities;

namespace VoxU_Backend.Core.Application.Mapper
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {

            //Publications
            CreateMap<Publications, SavePublicationRequest>()
                .ForMember(dest => dest.imageFile, opt => opt.Ignore())
                .ReverseMap()
              .ForMember(dest => dest.Comments, opt => opt.Ignore())
              .ForMember(dest => dest.Reports, opt => opt.Ignore());

            CreateMap<Publications, GetPublicationResponse>()
             .ForMember(dest => dest.CommentsCount, opt => opt.Ignore())
             .ForMember(dest => dest.Comments, opt => opt.Ignore())
              .ForMember(dest => dest.Reports, opt => opt.Ignore())
             .ReverseMap();

            //Comments
            CreateMap<Comments, SaveCommentsRequest>()
               .ReverseMap()
               .ForMember(dest => dest.replies, opt => opt.Ignore())
               .ForMember(dest => dest.Publications, opt => opt.Ignore());
               

            CreateMap<Comments, GetCommentsResponse>()
             .ReverseMap()
             .ForMember(dest => dest.replies, opt => opt.Ignore())
             .ForMember(dest => dest.Publications, opt => opt.Ignore());

            //Replies

            CreateMap<Replies, SaveRepliesRequest>()
                .ReverseMap()
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.Comments, opt => opt.Ignore());

            CreateMap<Replies, GetRepliesReponse>()
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
               .ReverseMap()
               .ForMember(dest => dest.Comments, opt => opt.Ignore());

            //Reports
            CreateMap<Report, SaveReportRequest>()
                .ReverseMap()
              .ForMember(dest => dest.Publications, opt => opt.Ignore());

            CreateMap<Report, GetReportResponse>();

        }
    }
}
