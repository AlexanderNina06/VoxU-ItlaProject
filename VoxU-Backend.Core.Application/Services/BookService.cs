using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.DTOS.Comments;
using VoxU_Backend.Core.Application.DTOS.Library;
using VoxU_Backend.Core.Application.Interfaces.Repositories;
using VoxU_Backend.Core.Application.Interfaces.Services;
using VoxU_Backend.Core.Domain.Entities;

namespace VoxU_Backend.Core.Application.Services
{
    public class BookService : GenericService<GetBookResponse, SaveBookRequest, Book>, IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public BookService(IMapper mapper, IBookRepository bookRepository) : base(mapper, bookRepository)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
    }
}
