using AutoMapper;
using MonoTask.Infrastructure.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POCO = MonoTask.Core.Entities;


namespace MonoTask.Infrastructure.DAL.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<VehicleMake, POCO.VehicleMake>();
            CreateMap<VehicleModel, POCO.VehicleModel>();
            
        }
    }
}
