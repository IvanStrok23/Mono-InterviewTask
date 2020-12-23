using MonoTask.Infrastructure.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTask.Infrastructure.DAL
{
    public class VehiclesDbContext : DbContext, IVehiclesDbContext
    {
        public DbSet<VehicleMake> VehiclesMake { get; set; }
        public DbSet<VehicleModel> VehiclesModel { get; set; }
    }
}
