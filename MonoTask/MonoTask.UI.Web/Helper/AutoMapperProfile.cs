using AutoMapper;
using MonoTask.Infrastructure.DAL.Entities;
using System.Linq;
using POCO = MonoTask.Core.Entities;


namespace MonoTask.Infrastructure.DAL.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<VehicleMakeEntity, POCO.VehicleMake>();
            CreateMap<POCO.VehicleMake, VehicleMakeEntity>();
            CreateMap<VehicleModelEntity, POCO.VehicleModel>()
                 .ForMember(m => m.VehicleMake, a => a.MapFrom(s => s.VehiceMake));
            CreateMap<POCO.VehicleModel, VehicleModelEntity>();
               
        }


    }

}
