using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.DTOS;
using VoxU_Backend.Core.Application.DTOS.SellPublication;
using VoxU_Backend.Core.Domain.Entities;

namespace VoxU_Backend.Core.Application.Interfaces.Services
{
    public interface ISellpublicationService : IGenericService<GetSellPublication, SaveSellPublication, SellPublications>
    {
    }
}
