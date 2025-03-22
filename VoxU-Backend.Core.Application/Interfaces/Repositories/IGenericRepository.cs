using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxU_Backend.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task<List<Entity>> GetAllAsync();
        Task<Entity> GetById(int Id);
        Task<List<Entity>> GetAllWithInclude(List<string> Property);
        Task<Entity> AddAsync(Entity entity);
        Task UpdateAsync(Entity entity);
        Task DeleteAsync(Entity entityDeleted);
    }
}
