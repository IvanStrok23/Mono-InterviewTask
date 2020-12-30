using MonoTask.Infrastructure.DAL.Entities;
using MonoTask.Infrastructure.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace MonoTask.Infrastructure.DAL
{
    public class VehiclesDbContext : DbContext, IVehiclesDbContext
    {
        public DbSet<VehicleMake> VehiclesMake { get; set; }
        public DbSet<VehicleModel> VehiclesModel { get; set; }


        public async Task<T> Get<T>(int id) where T : class, IEntity
        {
            var t = await base.Set<T>().FindAsync(id);
            return t;
        }

        public async Task<int> Insert<T>(T entity) where T : class, IEntity
        {
             base.Set<T>().Add(entity);
             await base.SaveChangesAsync();
             return entity.GetId();
        }

        public async Task Insert<T>(List<T> entities) where T : class, IEntity
        {
            base.Set<T>().AddRange(entities);
            await base.SaveChangesAsync();
        }

        public async Task<bool> Remove<T>(T entity) where T : class, IEntity
        {
            base.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            await base.SaveChangesAsync();
            return true;
            
        }

        public async Task Remove<T>(List<T> entities) where T : class, IEntity
        {
            base.Set<T>().RemoveRange(entities);
            await base.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await base.SaveChangesAsync();
        }
    }
}
