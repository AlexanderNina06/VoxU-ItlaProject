using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Domain.Entities;
using VoxU_Backend.Core.Application.DTOS.SellPublication;
using VoxU_Backend.Core.Application.DTOS;
using AutoMapper;
using VoxU_Backend.Core.Application.Interfaces.Repositories;
using VoxU_Backend.Core.Application.Interfaces.Services;

namespace VoxU_Backend.Core.Application.Services
{
    public class SellPublicationService : GenericService<GetSellPublication, SaveSellPublication ,SellPublications>, ISellpublicationService
    {
        private readonly ISellPublicationsRepository _sellsRepository;
        private readonly IMapper _mapper;
        public SellPublicationService(ISellPublicationsRepository sellsRepository, IMapper mapper) : base(mapper, sellsRepository)
        {
            _sellsRepository = sellsRepository;
            _mapper = mapper;
        }
        public async Task<List<GetSellPublication>> GetSellPublicationsByName(string Name)
        {
            var response = await _sellsRepository.GetSellPublicationsByName(Name);
            return response;
        }


    }
}
