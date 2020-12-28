using MonoTask.Infrastructure.DAL.Entities;
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
        //Task Insert<T>(T entity) where T:class;
        //Task Insert<T>(List<T> entities) where T:class;
        //Task Update<T>(IEntity entity) where T : class;

       // Task InsertVehicleModel(IEntity entity);
        Task<bool> Insert(POCO.VehicleModel entity);
        Task<POCO.VehicleModel> GetModelById(int id);
        Task<bool> EditModel(POCO.VehicleModel model);
        Task<List<POCO.VehicleModel>> GetModels(int page,string sortBy,string sortOrder,string searchValue);
        Task<int> GetCount(string searchValue);
        Task<bool> DeleteModel(int id);


    }
}
