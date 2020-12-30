using AutoMapper;
using MonoTask.Infrastructure.DAL.Entities;
using POCO = MonoTask.Core.Entities;


namespace MonoTask.Infrastructure.DAL.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<VehicleMake, POCO.VehicleMake>();
            CreateMap<POCO.VehicleMake, VehicleMake>();
            CreateMap<VehicleModel, POCO.VehicleModel>();
            CreateMap<POCO.VehicleModel, VehicleModel>();
            
        }
    }
}
