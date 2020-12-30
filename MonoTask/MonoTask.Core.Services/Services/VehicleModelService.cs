using AutoMapper;
using MonoTask.Core.Entities;
using MonoTask.Core.Services.Interfaces;
using MonoTask.Infrastructure.DAL.Entities;
using MonoTask.UI.Web.Helper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POCO = MonoTask.Core.Entities;


namespace MonoTask.Core.Services.Services
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly IVehiclesDbContext _vehiclesDbContext;
        private readonly IMapper _mapper;
        public VehicleModelService(IVehiclesDbContext vehiclesDbContext, IMapper mapper)
        {
            _vehiclesDbContext = vehiclesDbContext;
            _mapper = mapper;
        }

        public async Task<int> InsertModel(POCO.VehicleModel entity)
        {
            if (entity == null || entity.Id != 0)
            {
                return 0;
            }
            VehicleModelEntity mapped = _mapper.Map<VehicleModelEntity>(entity);
            mapped.CreatedAt = DateTime.UtcNow;
            mapped.UpdatedAt = DateTime.UtcNow;
            return await _vehiclesDbContext.Insert(mapped);
        }

        public async Task<POCO.VehicleModel> GetModelById(int id)
        {
            var query = from model in _vehiclesDbContext.VehiclesModel
                        join make in _vehiclesDbContext.VehiclesMake on model.MakeId equals make.Id into mo
                        from makeProp in mo.DefaultIfEmpty()
                        where model.Id == id
                        select new POCO.VehicleModel()
                        {
                            Id = model.Id,
                            Name = model.Name,
                            MakeId = model.MakeId,
                            MakeName = makeProp.Name == null ? "Undefinded" : makeProp.Name,
                            Year = model.Year,
                            CreatedAt = model.CreatedAt,
                            UpdatedAt = model.UpdatedAt
                        };
            return await Task.Run(() => query.FirstOrDefaultAsync()); ;
        }

        public async Task<List<POCO.VehicleModel>> GetModels(SortingData sortingData)
        {
            var query = from model in _vehiclesDbContext.VehiclesModel
                        join make in _vehiclesDbContext.VehiclesMake on model.MakeId equals make.Id into mo
                        from makeProp in mo.DefaultIfEmpty()
                        select new POCO.VehicleModel()
                        {
                            Id = model.Id,
                            Name = model.Name,
                            MakeId = model.MakeId,
                            MakeName = makeProp.Name == null ? "Undefinded" : makeProp.Name,
                            Year = model.Year,
                            CreatedAt = model.CreatedAt,
                            UpdatedAt = model.UpdatedAt
                        };

            switch (sortingData.SortBy)
            {
                case "Name":
                    query = sortingData.SortOrder == "desc" ? query.OrderByDescending(i => i.Name) : query.OrderBy(i => i.Name);
                    break;
                case "Make":
                    query = sortingData.SortOrder == "desc" ? query.OrderByDescending(i => i.MakeName) : query.OrderBy(i => i.MakeName);
                    break;
                case "Year":
                    query = sortingData.SortOrder == "desc" ? query.OrderByDescending(i => i.Year) : query.OrderBy(i => i.Year);
                    break;
                default:
                    query = sortingData.SortOrder == "desc" ? query.OrderByDescending(i => i.Name) : query.OrderBy(i => i.Name);
                    break;
            }

            if (!String.IsNullOrWhiteSpace(sortingData.SearchValue))
            {
                query = query.Where(i => i.Name.StartsWith(sortingData.SearchValue));
            }
            query = query.Skip((sortingData.Page - 1) * 10).Take(10);

            return await Task.Run(() => query.ToListAsync());
        }
        public async Task<int> GetModelCount(string searchValue)
        {
            return await Task.Run(() => _vehiclesDbContext.VehiclesModel.Where(i => i.Name.StartsWith(searchValue)).Count());
        }
        public async Task<bool> EditModel(POCO.VehicleModel model)
        {
            VehicleModelEntity temp = _vehiclesDbContext.VehiclesModel.Where(i => i.Id == model.Id).FirstOrDefault();
            if (temp == null)
            {
                return false;
            }
            else
            {
                temp.Name = model.Name;
                temp.MakeId = model.MakeId;
                temp.Year = model.Year;
                temp.UpdatedAt = DateTime.UtcNow;
                await _vehiclesDbContext.SaveAsync();
                return true;
            }
        }

        public async Task<bool> DeleteModel(int id)
        {
            var m = await Task.Run(() => _vehiclesDbContext.VehiclesModel.FindAsync(id));
            if (m == null)
            {
                return false;
            }
            return await _vehiclesDbContext.Remove(m);
        }


    }
}
