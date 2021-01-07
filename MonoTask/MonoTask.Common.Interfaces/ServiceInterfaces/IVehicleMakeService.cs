using MonoTask.Core.Entities;
using MonoTask.UI.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTask.Common.Interfaces.ServiceInterfaces
{
    public interface IVehicleMakeService
    {
        Task<int> InsertMake(VehicleMake entity);
        Task<VehicleMake> GetMakeById(int id);
        Task<bool> EditMake(VehicleMake model);
        Task<List<VehicleMake>> GetMakes();
        Task<List<VehicleMake>> GetMakesSortedByColumn(TableFilterData filterData);
        Task<List<VehicleMake>> GetMakesByPage(TableFilterData filterData);
        Task<List<VehicleMake>> GetMakesByName(TableFilterData filterData);
        Task<int> GetMakeCount(string searchValue = "");
        Task<bool> DeleteMake(int id);
        Task<Dictionary<int, string>> GetMakeDropdown();

    }
}
