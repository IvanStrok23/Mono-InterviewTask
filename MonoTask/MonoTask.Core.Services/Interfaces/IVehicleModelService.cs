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
        Task<List<POCO.VehicleModel>> GetModels(SortingData sortingData);
        Task<int> GetModelCount(string searchValue);
        Task<bool> DeleteModel(int id);

    }
}
