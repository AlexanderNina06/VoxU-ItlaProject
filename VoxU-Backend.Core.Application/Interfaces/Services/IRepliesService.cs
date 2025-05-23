﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.DTOS.Replies;
using VoxU_Backend.Core.Domain.Entities;

namespace VoxU_Backend.Core.Application.Interfaces.Services
{
    public interface IRepliesService : IGenericService<GetRepliesReponse, SaveRepliesRequest, Replies>
    {
    }
}
