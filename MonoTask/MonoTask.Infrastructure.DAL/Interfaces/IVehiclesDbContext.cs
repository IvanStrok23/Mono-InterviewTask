using MonoTask.Infrastructure.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTask.Infrastructure.DAL.Entities
{
  public interface IVehiclesDbContext
    {
         DbSet<VehicleMake> VehiclesMake { get; set; }
         DbSet<VehicleModel> VehiclesModel { get; set; }

        Task SaveAsync();
        Task Insert<T>(T entity) where T : class;
        Task Insert<T>(List<T> entities) where T : class;

        Task<bool> Remove<T>(T entity) where T : class;
        Task Remove<T>(List<T> entities) where T : class;
        Task<T> Get<T>(int id) where T : class;
    }
}
