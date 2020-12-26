using MonoTask.Infrastructure.DAL.Entities;
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
        POCO.VehicleModel GetTest();
    }
}
