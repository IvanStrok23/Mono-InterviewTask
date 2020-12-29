﻿using MonoTask.Infrastructure.DAL.Entities;
using MonoTask.Infrastructure.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POCO = MonoTask.Core.Entities;

namespace MonoTask.Core.Services
{
    public interface IVehicleService
    {

        Task<bool> InsertModel(POCO.VehicleModel entity);
        Task<bool> InsertMake(POCO.VehicleMake entity);
        Task<POCO.VehicleModel> GetModelById(int id);
        Task<POCO.VehicleMake> GetMakeById(int id);
        Task<bool> EditModel(POCO.VehicleModel model);
        Task<bool> EditMake(POCO.VehicleMake model);
        Task<List<POCO.VehicleModel>> GetModels(int page,string sortBy,string sortOrder,string searchValue);
        Task<List<POCO.VehicleMake>> GetMakes(int page,string sortBy,string sortOrder,string searchValue);
        Task<Dictionary<int, string>> GetMakeDropdown();
        Task<int> GetModelCount(string searchValue);
        Task<int> GetMakeCount(string searchValue);
        Task<bool> DeleteModel(int id);
        Task<bool> DeleteMake(int id);


    }
}
