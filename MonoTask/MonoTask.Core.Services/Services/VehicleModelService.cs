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
            var query = await _vehiclesDbContext.VehiclesModel.Where(m => m.Id == id).Include(m => m.VehiceMake).FirstOrDefaultAsync();
            return _mapper.Map<POCO.VehicleModel>(query);
        }



        public async Task<List<POCO.VehicleModel>> GetModelsSortedByColumn(TableFilterData filterData)
        {

            var query = _vehiclesDbContext.VehiclesModel.
            Include(m => m.VehiceMake);
            sortByColumn(ref query, filterData.SortBy, filterData.SortOrder);
            searchByName(ref query, filterData.SearchValue);
            var res = await query.Take(10).ToListAsync();
            return _mapper.Map<List<POCO.VehicleModel>>(res);

        }

        public async Task<List<POCO.VehicleModel>> GetModelsByPage(TableFilterData filterData)
        {

            var query =  _vehiclesDbContext.VehiclesModel.
            Include(m => m.VehiceMake);
            sortByColumn(ref query, filterData.SortBy, filterData.SortOrder);
            searchByName(ref query, filterData.SearchValue);
            return _mapper.Map<List<POCO.VehicleModel>>(query.Skip((filterData.Page - 1) * 10).Take(10).ToListAsync());

        }

        public async Task<List<POCO.VehicleModel>> GetModelsByName(TableFilterData filterData)
        {

            var query = await _vehiclesDbContext.VehiclesModel.
                Include(m => m.VehiceMake).
                OrderBy(i => i.Name).
                Where(i => i.Name.StartsWith(filterData.SearchValue)).
                Take(10).ToListAsync();
            return _mapper.Map<List<POCO.VehicleModel>>(query);

        }


        public async Task<List<POCO.VehicleModel>> GetModels()
        {
            var query = await _vehiclesDbContext.VehiclesModel.Include(m => m.VehiceMake).Take(10).ToListAsync();
            return _mapper.Map<List<POCO.VehicleModel>>(query);
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
                temp.VehiceMake = _vehiclesDbContext.VehiclesMake.Where(i => i.Id == model.MakeId).FirstOrDefault();
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


        private void sortByColumn(ref IQueryable<VehicleModelEntity> query, string sortBy, string sortOrder)
        {

            switch (sortBy)
            {
                case "Name":
                    query = sortOrder == "desc" ? query.OrderByDescending(i => i.Name) : query.OrderBy(i => i.Name);
                    break;
                case "Make":
                    query = sortOrder == "desc" ? query.OrderByDescending(i => i.VehiceMake.Name) : query.OrderBy(i => i.VehiceMake.Name);
                    break;
                case "Year":
                    query = sortOrder == "desc" ? query.OrderByDescending(i => i.Year) : query.OrderBy(i => i.Year);
                    break;
                default:
                    query = sortOrder == "desc" ? query.OrderByDescending(i => i.Name) : query.OrderBy(i => i.Name);
                    break;
            }
        }

        private void searchByName(ref IQueryable<VehicleModelEntity> query, string searchValue)
        {

            if (!String.IsNullOrWhiteSpace(searchValue))
            {
                query = query.Where(i => i.Name.StartsWith(searchValue));
            }
        }
    }
}
