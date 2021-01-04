using AutoMapper;
using MonoTask.Infrastructure.DAL.Entities;
using System.Collections.Generic;
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
            CreateMap<VehicleModelEntity, POCO.VehicleModel>();

            CreateMap<POCO.VehicleModel, VehicleModelEntity>();
            CreateMap<KeyValuePair<string, VehicleModelEntity>, POCO.VehicleModel>()
                .ConstructUsing(kvp => new POCO.VehicleModel()
                {
                    Id = kvp.Value.Id,
                    Name = kvp.Value.Name,
                    MakeId = kvp.Value.MakeId,
                    MakeName = kvp.Key == null ? "Undefinded" : kvp.Key,
                    Year = kvp.Value.Year,
                    CreatedAt = kvp.Value.CreatedAt,
                    UpdatedAt = kvp.Value.UpdatedAt
                });

        }


    }

}
