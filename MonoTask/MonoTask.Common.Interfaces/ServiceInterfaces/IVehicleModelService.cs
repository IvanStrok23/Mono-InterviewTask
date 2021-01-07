using MonoTask.Core.Entities;
using MonoTask.UI.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTask.Common.Interfaces.ServiceInterfaces
{
    public interface IVehicleModelService
    {
        Task<int> InsertModel(VehicleModel entity);
        Task<VehicleModel> GetModelById(int id);
        Task<bool> EditModel(VehicleModel model);
        Task<List<VehicleModel>> GetModels();
        Task<List<VehicleModel>> GetModelsSortedByColumn(TableFilterData filterData);
        Task<List<VehicleModel>> GetModelsByPage(TableFilterData filterData);
        Task<List<VehicleModel>> GetModelsByName(TableFilterData filterData);
        Task<int> GetModelCount(string searchValue = "");
        Task<bool> DeleteModel(int id);

    }
}
