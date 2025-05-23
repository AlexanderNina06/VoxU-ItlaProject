﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.Interfaces.Repositories;
using VoxU_Backend.Core.Domain.Entities;
using VoxU_Backend.Core.Persistence.Contexts;

namespace VoxU_Backend.Core.Persistence.Repositories
{
    public class SellPublicationsRepository : GenericRepository<SellPublications>, ISellPublicationsRepository
    {
        private readonly ApplicationContext _applicationContext;
        public SellPublicationsRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            _applicationContext = applicationContext;
        }
    }
}
