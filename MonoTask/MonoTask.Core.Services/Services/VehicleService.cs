using MonoTask.Infrastructure.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTask.Core.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehiclesDbContext _vehiclesDbContext;
        public VehicleService(IVehiclesDbContext vehiclesDbContext)
        {
            _vehiclesDbContext = vehiclesDbContext;
        }

        public VehicleModel GetTest()
        {
            return new VehicleModel()
            {
                Id = 4,
                MakeId = 3
            };
        }
    }
}
