using AutoMapper;
using MonoTask.Infrastructure.DAL.Entities;
using POCO = MonoTask.Core.Entities;


namespace MonoTask.Infrastructure.DAL.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<VehicleMakeEntity, POCO.VehicleMake>();
            CreateMap<POCO.VehicleMake, VehicleMakeEntity>();
            CreateMap<VehicleModelEntity, POCO.VehicleModel>();
            CreateMap<POCO.VehicleModel, VehicleModelEntity>();
            
        }
    }
}
