using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.DTOS;
using VoxU_Backend.Core.Application.Interfaces.Repositories;
using VoxU_Backend.Core.Domain.Entities;
using VoxU_Backend.Core.Persistence.Contexts;

namespace VoxU_Backend.Core.Persistence.Repositories
{
    public class SellPublicationsRepository : GenericRepository<SellPublications>, ISellPublicationsRepository
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;
        
        public SellPublicationsRepository(ApplicationContext applicationContext, IMapper mapper) : base(applicationContext)
        {
            _applicationContext = applicationContext;
            _mapper = mapper;
        }
        public async Task<List<GetSellPublication>> GetSellPublicationsByName(string Name, int? CategoryId)
        {
            if (CategoryId != null && CategoryId > 0)
            {
                var requestData = await _applicationContext.SellPublications.Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{Name.ToLower()}%") && x.CategoryId == CategoryId).ToListAsync();
                var response = _mapper.Map<List<GetSellPublication>>(requestData);
                return response;
            }
            else
            {
                var requestData = await _applicationContext.SellPublications.Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{Name.ToLower()}%")).ToListAsync();
                var response = _mapper.Map<List<GetSellPublication>>(requestData);
                return response;
            }
        }
    }  
}
