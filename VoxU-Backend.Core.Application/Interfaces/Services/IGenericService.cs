using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxU_Backend.Core.Application.Interfaces.Services
{
    public interface IGenericService<ViewModel, SaveViewModel, Entity>
        where ViewModel : class
        where SaveViewModel : class
        where Entity : class
    {
        Task<SaveViewModel> AddAsyncVm(SaveViewModel saveViewModel);
        Task DeleteVmAsync(int Id);
        Task<List<ViewModel>> GetAllVm();
        Task<ViewModel> GetVmById(int Id);
        Task UpdateAsyncVm(SaveViewModel saveViewModel, int Id);
    }
}
