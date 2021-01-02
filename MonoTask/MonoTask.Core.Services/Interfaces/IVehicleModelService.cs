using MonoTask.UI.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POCO = MonoTask.Core.Entities;


namespace MonoTask.Core.Services.Interfaces
{
    public interface IVehicleModelService
    {
        Task<int> InsertModel(POCO.VehicleModel entity);
        Task<POCO.VehicleModel> GetModelById(int id);
        Task<bool> EditModel(POCO.VehicleModel model);
        Task<List<POCO.VehicleModel>> GetModels();
        Task<List<POCO.VehicleModel>> GetModelsSortedByColumn(TableFilterData filterData);
        Task<List<POCO.VehicleModel>> GetModelsByPage(TableFilterData filterData);
        Task<List<POCO.VehicleModel>> GetModelsByName(TableFilterData filterData);
        Task<int> GetModelCount(string searchValue = "");
        Task<bool> DeleteModel(int id);

    }
}
