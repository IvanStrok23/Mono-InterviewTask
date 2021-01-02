using AutoMapper;
using MonoTask.Core.Entities;
using MonoTask.UI.Web.Helper;
using MonoTask.Core.Services.Interfaces;
using MonoTask.Infrastructure.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            return await Task.Run(() => query.FirstOrDefaultAsync());
        }



        public async Task<List<POCO.VehicleModel>> GetModelsSortedByColumn(TableFilterData filterData)
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

            sortByColumn(ref query, filterData.SortBy,filterData.SortOrder);
            searchByName(ref query, filterData.SearchValue);           
            return await Task.Run(() => query.Take(10).ToListAsync());
        }

        public async Task<List<POCO.VehicleModel>> GetModelsByPage(TableFilterData filterData)
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
           // sortByColumn(ref query, filterData.SortBy, filterData.SortOrder);
            searchByName(ref query, filterData.SearchValue);
            switch (filterData.SortBy)
            {
                case "Name":
                    query = filterData.SortOrder == "desc" ? query.OrderByDescending(i => i.Name) : query.OrderBy(i => i.Name);
                    break;
                case "Make":
                    query = filterData.SortOrder == "desc" ? query.OrderByDescending(i => i.MakeName) : query.OrderBy(i => i.MakeName);
                    break;
                case "Year":
                    query = filterData.SortOrder == "desc" ? query.OrderByDescending(i => i.Year) : query.OrderBy(i => i.Year);
                    break;
                default:
                    query = filterData.SortOrder == "desc" ? query.OrderByDescending(i => i.Name) : query.OrderBy(i => i.Name);
                    break;
            }
            return await Task.Run(() => query.Skip((filterData.Page - 1) * 10).Take(10).ToListAsync());
        }

        public async Task<List<POCO.VehicleModel>> GetModelsByName(TableFilterData filterData)
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
            return await Task.Run(() => query.OrderBy(i => i.Name).Where(i => i.Name.StartsWith(filterData.SearchValue)).Take(10).ToListAsync());
        }


        public async Task<List<POCO.VehicleModel>> GetModels()
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
            return await Task.Run(() => query.OrderBy(i => i.Name).Take(10).ToListAsync());
        }

        public async Task<int> GetModelCount(string searchValue = "")
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


        private void sortByColumn(ref IQueryable<VehicleModel> query,string sortBy,string sortOrder)
        {

            switch (sortBy)
            {
                case "Name":
                    query = sortOrder == "desc" ? query.OrderByDescending(i => i.Name) : query.OrderBy(i => i.Name);
                    break;
                case "Make":
                    query = sortOrder == "desc" ? query.OrderByDescending(i => i.MakeName) : query.OrderBy(i => i.MakeName);
                    break;
                case "Year":
                    query = sortOrder == "desc" ? query.OrderByDescending(i => i.Year) : query.OrderBy(i => i.Year);
                    break;
                default:
                    query = sortOrder == "desc" ? query.OrderByDescending(i => i.Name) : query.OrderBy(i => i.Name);
                    break;
            }
        }
        
        private void searchByName(ref IQueryable<VehicleModel> query,string searchValue)
        {

            if (!String.IsNullOrWhiteSpace(searchValue))
            {
                query = query.Where(i => i.Name.StartsWith(searchValue));
            }
        }

    }
    public static class MappingExtensions
    {

        public static TDestination Map<TSource1, TSource2, TDestination>(
         this IMapper mapper, TSource1 source1, TSource2 source2)
        {
            var destination = mapper.Map<TSource1, TDestination>(source1);
            return mapper.Map(source2, destination);
        }


        public static TDestination Map<TSource, TDestination>(
            this IMapper mapper, TSource source)
        {
            return mapper.Map<TSource, TDestination>(source);
           // return mapper.Map(source2, destination);
        }


    }
}
