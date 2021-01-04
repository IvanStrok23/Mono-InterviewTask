﻿using AutoMapper;
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
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly IVehiclesDbContext _vehiclesDbContext;
        private readonly IMapper _mapper;
        public VehicleMakeService(IVehiclesDbContext vehiclesDbContext, IMapper mapper)
        {
            _vehiclesDbContext = vehiclesDbContext;
            _mapper = mapper;
        }

        public async Task<int> InsertMake(POCO.VehicleMake entity)
        {
            if (entity == null || entity.Id != 0)
            {
                return 0;
            }
            VehicleMakeEntity mapped = _mapper.Map<VehicleMakeEntity>(entity);
            mapped.CreatedAt = DateTime.UtcNow;
            mapped.UpdatedAt = DateTime.UtcNow;
            return await _vehiclesDbContext.Insert(mapped);
        }

        public async Task<POCO.VehicleMake> GetMakeById(int id)
        {
            var result = await Task.Run(() => _vehiclesDbContext.Get<VehicleMakeEntity>(id));
            return _mapper.Map<POCO.VehicleMake>(result);
        }

        public async Task<List<POCO.VehicleMake>> GetMakesSortedByColumn(TableFilterData filterData)
        {

            var query = from make in _vehiclesDbContext.VehiclesMake select make;
            sortByColumn(ref query, filterData.SortBy, filterData.SortOrder);
            var result = await Task.Run(() => query.Where(i => i.Name.StartsWith(filterData.SearchValue)).Take(10).ToListAsync());
            return _mapper.Map<List<POCO.VehicleMake>>(result);
        }

        public async Task<List<POCO.VehicleMake>> GetMakesByPage(TableFilterData filterData)
        {
            var query = from make in _vehiclesDbContext.VehiclesMake select make;
            sortByColumn(ref query, filterData.SortBy, filterData.SortOrder);
            var result = await Task.Run(() => query.Where(i => i.Name.StartsWith(filterData.SearchValue)).Skip((filterData.Page - 1) * 10).Take(10).ToListAsync());
            return _mapper.Map<List<POCO.VehicleMake>>(result);
        }

        public async Task<List<POCO.VehicleMake>> GetMakesByName(TableFilterData filterData)
        {
            var result = await Task.Run(() => _vehiclesDbContext.VehiclesMake.OrderBy(i => i.Name).Where(i => i.Name.StartsWith(filterData.SearchValue)).Take(10).ToListAsync());
            return _mapper.Map<List<POCO.VehicleMake>>(result);
        }



        public async Task<List<POCO.VehicleMake>> GetMakes()
        {
            var result = await Task.Run(() => _vehiclesDbContext.VehiclesMake.OrderBy(i => i.Name).Take(10).ToListAsync());
            return _mapper.Map<List<POCO.VehicleMake>>(result);
        }

        public async Task<int> GetMakeCount(string searchValue = "")
        {
            return await Task.Run(() => _vehiclesDbContext.VehiclesMake.Where(i => i.Name.StartsWith(searchValue)).Count());
        }

        public async Task<Dictionary<int, string>> GetMakeDropdown()
        {
            //TODO: This should be limited

            var query = _vehiclesDbContext.VehiclesMake.ToDictionaryAsync(x => x.Id, x => x.Name);
            return await Task.Run(() => query);
        }

        public async Task<bool> EditMake(POCO.VehicleMake model)
        {
            VehicleMakeEntity temp = _vehiclesDbContext.VehiclesMake.Where(i => i.Id == model.Id).FirstOrDefault();
            if (temp == null)
            {
                return false;
            }
            else
            {
                temp.Name = model.Name;
                temp.Country = model.Country;
                temp.UpdatedAt = DateTime.UtcNow;
                await _vehiclesDbContext.SaveAsync();
                return true;
            }
        }

        public async Task<bool> DeleteMake(int id)
        {
            //TODO: Remove all Model objects that are maked from that maker if needed 

            var m = await Task.Run(() => _vehiclesDbContext.VehiclesMake.FindAsync(id));
            if (m == null)
            {
                return false;
            }
            return await _vehiclesDbContext.Remove(m);
        }

        private void sortByColumn(ref IQueryable<VehicleMakeEntity> query, string sortBy, string sortOrder)
        {

            switch (sortBy)
            {
                case "Name":
                    query = sortOrder == "desc" ? query.OrderByDescending(i => i.Name) : query.OrderBy(i => i.Name);
                    break;
                case "Country":
                    query = sortOrder == "desc" ? query.OrderByDescending(i => i.Country) : query.OrderBy(i => i.Country);
                    break;
                default:
                    query = sortOrder == "desc" ? query.OrderByDescending(i => i.Name) : query.OrderBy(i => i.Name);
                    break;
            }
        }
    }

}
