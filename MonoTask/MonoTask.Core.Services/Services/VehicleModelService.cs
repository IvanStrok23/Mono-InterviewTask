﻿using MonoTask.Infrastructure.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTask.Core.Services
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly IVehiclesDbContext _vehiclesDbContext;
        public VehicleModelService(IVehiclesDbContext vehiclesDbContext)
        {
            _vehiclesDbContext = vehiclesDbContext;
        }
    }
}
