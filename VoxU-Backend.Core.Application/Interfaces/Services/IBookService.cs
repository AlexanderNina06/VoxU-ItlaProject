using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.DTOS.Library;
using VoxU_Backend.Core.Domain.Entities;

namespace VoxU_Backend.Core.Application.Interfaces.Services
{
    public interface IBookService : IGenericService<GetBookResponse, SaveBookRequest, Book>
    {
        Task<(bool Success, string Message, string Url)> UploadDocumentAsync(IFormFile file);
        Task<List<GetBookResponse>> GetDocumentsAsync();
    }
}
