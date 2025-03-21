using AutoMapper;
using VoxU_Backend.Core.Application.Interfaces.Repositories;
using VoxU_Backend.Core.Application.Interfaces.Services;

namespace VoxU_Backend.Core.Application.Services
{
    public class GenericService<ViewModel, SaveViewModel, Entity> : IGenericService<ViewModel, SaveViewModel, Entity>
        where ViewModel : class
        where SaveViewModel : class
        where Entity : class
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Entity> _genericRepository;
        public GenericService(IMapper mapper, IGenericRepository<Entity> genericRepository)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
        }

        public virtual async Task<List<ViewModel>> GetAllVm()
        {
            List<Entity> entities = await _genericRepository.GetAllAsync();

            var VmList = _mapper.Map<List<ViewModel>>(entities);
            return VmList;
        }
        public virtual async Task<ViewModel> GetVmById(int Id)
        {
            Entity Entity = await _genericRepository.GetById(Id);

            ViewModel Vm = _mapper.Map<ViewModel>(Entity);

            return Vm;
        }


        public virtual async Task<SaveViewModel> AddAsyncVm(SaveViewModel saveViewModel)
        {
            Entity entity = _mapper.Map<Entity>(saveViewModel);

            var addedEntity = await _genericRepository.AddAsync(entity);

            SaveViewModel saveViewModel1 = _mapper.Map<SaveViewModel>(addedEntity);

            return saveViewModel1;
        }

        public virtual async Task Update(SaveViewModel vm)
        {
            Model entity = _mapper.Map<Model>(vm);

            await _repository.UpdateAsync(entity);
        }

        public virtual async Task DeleteVmAsync(int Id)
        {
            Entity EntityToDelete = await _genericRepository.GetById(Id);
            await _genericRepository.DeleteAsync(EntityToDelete);

        }

    }
}
