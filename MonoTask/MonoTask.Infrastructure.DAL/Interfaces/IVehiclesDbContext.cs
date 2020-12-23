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
    }
}
