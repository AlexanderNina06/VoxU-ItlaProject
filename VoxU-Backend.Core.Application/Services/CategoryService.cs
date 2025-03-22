using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.DTOS.SellPublication;
using VoxU_Backend.Core.Application.DTOS;
using VoxU_Backend.Core.Application.Interfaces.Repositories;
using VoxU_Backend.Core.Application.Interfaces.Services;
using VoxU_Backend.Core.Domain.Entities;
using VoxU_Backend.Core.Application.DTOS.Category;

namespace VoxU_Backend.Core.Application.Services
{
    public class CategoryService : GenericService<GetCategoryResponse, SaveCategoryRequest, Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper) : base(mapper, categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
    }
}
