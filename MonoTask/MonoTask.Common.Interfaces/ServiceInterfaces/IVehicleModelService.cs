using MonoTask.Core.Entities;
using MonoTask.Core.Entities.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonoTask.Common.Interfaces.ServiceInterfaces
{
    public interface IVehicleModelService
    {
        Task<int> InsertModel(VehicleModel entity);
        Task<VehicleModel> GetModelById(int id);
        Task<bool> EditModel(VehicleModel model);
        Task<List<VehicleModel>> GetModels();
        Task<List<VehicleModel>> GetModelsSortedByColumn(SortParams sortParams);
        Task<List<VehicleModel>> GetModelsByPage(PagingParams pagingParams);
        Task<List<VehicleModel>> GetModelsByName(string searchValue);
        Task<int> GetModelCount(string searchValue = "");
        Task<bool> DeleteModel(int id);

    }
}
