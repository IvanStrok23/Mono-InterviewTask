﻿using MonoTask.UI.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POCO = MonoTask.Core.Entities;

namespace MonoTask.Core.Services.Interfaces
{
    public interface IVehicleMakeService
    {
        Task<int> InsertMake(POCO.VehicleMake entity);
        Task<POCO.VehicleMake> GetMakeById(int id);
        Task<bool> EditMake(POCO.VehicleMake model);
        Task<List<POCO.VehicleMake>> GetMakes(SortingData sortingData);
        Task<int> GetMakeCount(string searchValue);
        Task<bool> DeleteMake(int id);
        Task<Dictionary<int, string>> GetMakeDropdown();

    }
}