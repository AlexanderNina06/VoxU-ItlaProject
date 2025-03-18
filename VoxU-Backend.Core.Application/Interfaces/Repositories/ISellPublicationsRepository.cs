﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.DTOS;
using VoxU_Backend.Core.Domain.Entities;

namespace VoxU_Backend.Core.Application.Interfaces.Repositories
{
    public interface ISellPublicationsRepository : IGenericRepository<SellPublications>
    {
        Task<List<GetSellPublication>> GetSellPublicationsByName(string Name, int? CategoryId);
    }
}
