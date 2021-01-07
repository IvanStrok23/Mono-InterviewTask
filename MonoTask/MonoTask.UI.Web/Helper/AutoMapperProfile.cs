using AutoMapper;
using MonoTask.Infrastructure.DAL.Entities;
using MonoTask.UI.Web.Models;
using System.Linq;
using POCO = MonoTask.Core.Entities;


namespace MonoTask.Infrastructure.DAL.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //i: EF to POCO
            CreateMap<VehicleMakeEntity, POCO.VehicleMake>();
            CreateMap<VehicleModelEntity, POCO.VehicleModel>()
               .ForMember(m => m.VehicleMake, a => a.MapFrom(s => s.VehiceMake));

            //i: POCO to EF
            CreateMap<POCO.VehicleMake, VehicleMakeEntity>();
            CreateMap<POCO.VehicleModel, VehicleModelEntity>();

            //i:  POCO to ViewModel
            CreateMap<POCO.VehicleModel, VehicleModelView>()
                .ForMember(m => m.MakeName, a => a.MapFrom(s => s.VehicleMake == null ? "" :s.VehicleMake.Name))
                .ForMember(m => m.MakeId, a => a.MapFrom(s => s.VehicleMake.Id));
            CreateMap<POCO.VehicleMake, VehicleMakeView>();


            //i: ViewModel to POCO
            CreateMap<VehicleModelView, POCO.VehicleModel>()
                .ForMember(m => m.MakeId, a => a.MapFrom(s => s.MakeId));             
            CreateMap<POCO.VehicleMake, VehicleMakeView>();

        }


    }

}
