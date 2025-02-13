using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.DTOS.Publication;
using VoxU_Backend.Core.Domain.Entities;

namespace VoxU_Backend.Core.Application.Mapper
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Publications, SavePublicationRequest>()
                .ReverseMap()
              .ForMember(dest => dest.Comments, opt => opt.Ignore());

            CreateMap<Publications, GetPublicationResponse>()
             .ForMember(dest => dest.userPicture, opt => opt.Ignore())
             .ForMember(dest => dest.userName, opt => opt.Ignore())
             .ForMember(dest => dest.CommentsCount, opt => opt.Ignore())
             .ReverseMap();
        }
    }
}
