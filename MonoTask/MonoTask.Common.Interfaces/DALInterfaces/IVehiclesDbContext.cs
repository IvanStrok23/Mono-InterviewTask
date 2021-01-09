using MonoTask.Core.Entities.Interface;
using MonoTask.Infrastructure.DAL.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace MonoTask.Infrastructure.DAL.Interfaces
{
    public interface IVehiclesDbContext
    {
        DbSet<VehicleMakeEntity> VehiclesMake { get; set; }
        DbSet<VehicleModelEntity> VehiclesModel { get; set; }
        Task<T> Get<T>(int id) where T : class, IEntity;
        Task<int> Insert<T>(T entity) where T : class, IEntity;
        Task Insert<T>(List<T> entities) where T : class, IEntity;
        Task<bool> Remove<T>(T entity) where T : class, IEntity;
        Task Remove<T>(List<T> entities) where T : class, IEntity;
        Task SaveAsync();
    }
}
