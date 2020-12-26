using AutoMapper;
using AutoMapper.QueryableExtensions;
using MonoTask.Infrastructure.DAL.AutoMapper;
using MonoTask.Infrastructure.DAL.Entities;
using System.Linq;
using POCO = MonoTask.Core.Entities;


namespace MonoTask.Core.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehiclesDbContext _vehiclesDbContext;
        private readonly IMapper _mapper;
        public VehicleService(IVehiclesDbContext vehiclesDbContext,IMapper mapper)
        {
            _vehiclesDbContext = vehiclesDbContext;
            _mapper = mapper;
        }

        public POCO.VehicleModel GetTest()
        {
            VehicleModel item2 = _vehiclesDbContext.VehiclesModel.FirstOrDefault();
            return _vehiclesDbContext.VehiclesModel.ProjectTo<POCO.VehicleModel>(_mapper.ConfigurationProvider).Where(i => i.Year == 2019).FirstOrDefault();
        }

        //public async Task Add(VehicleModel employee)
        //{
        //    employee.Id = Guid.NewGuid().;
        //    db.Employees.Add(employee);
        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

    }
}
