using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Persistence.Contexts;


namespace VoxU_Backend.Core.Persistence.Repositories
{
    public class GenericRepository<Entity> where Entity : class
    {
        private readonly ApplicationContext _applicationContext;
        public GenericRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public virtual async Task<List<Entity>> GetAllAsync()
        {
            List<Entity> entities = await _applicationContext.Set<Entity>().ToListAsync();
            return entities;
        }

        public virtual async Task<Entity> GetById(int Id)
        {
            Entity entity = await _applicationContext.Set<Entity>().FindAsync(Id);
            return entity;
        }
        public virtual async Task<List<Entity>> GetAllWithInclude(List<string> navigationProperties)
        {
            var query = _applicationContext.Set<Entity>().AsQueryable();

            foreach (string property in navigationProperties)
            {
                query = query.Include(property);
            }
            var querylist = await query.ToListAsync();
            return querylist;
        }

        public virtual async Task<Entity> AddAsync(Entity entity)
        {
            await _applicationContext.Set<Entity>().AddAsync(entity);
            await _applicationContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task UpdateAsync(Entity entityUpdated, int Id)
        {
            Entity entry = await _applicationContext.Set<Entity>().FindAsync(Id);
            _applicationContext.Entry(entry).CurrentValues.SetValues(entityUpdated);
            await _applicationContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Entity entityDeleted)
        {
            _applicationContext.Set<Entity>().Remove(entityDeleted);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
