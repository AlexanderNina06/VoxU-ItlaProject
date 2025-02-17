using VoxU_Backend.Core.Application.Interfaces.Repositories;
using VoxU_Backend.Core.Domain.Entities;
using VoxU_Backend.Core.Persistence.Contexts;

namespace VoxU_Backend.Core.Persistence.Repositories
{
    public class CommentsRepository : GenericRepository<Comments>, ICommentsRepository
    {
        private readonly ApplicationContext _context;

        public CommentsRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            _context = applicationContext;
        }
    }
}
