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
            CreateMap<VehicleModelEntity, POCO.VehicleModel>();
            CreateMap<POCO.VehicleModel, VehicleModelEntity>();

            //CreateMap<VehicleMakeEntity, POCO.VehicleModel>()
            // .ForMember(d => d.MakeName, a => a.MapFrom(s => s.Name));
        }


    }

}
