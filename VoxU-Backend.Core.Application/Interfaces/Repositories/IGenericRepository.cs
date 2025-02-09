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
        Task UpdateAsync(Entity entityUpdated, int Id);
        Task DeleteAsync(Entity entityDeleted);
    }
}
