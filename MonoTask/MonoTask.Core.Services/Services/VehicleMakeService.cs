using AutoMapper;
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
        public async Task<List<POCO.VehicleMake>> GetMakes(SortingData sortingData)
        {
            var query = _vehiclesDbContext.VehiclesMake.Where(i => i.Id != null);

            switch (sortingData.SortBy)
            {
                case "Name":
                    query = sortingData.SortOrder == "desc" ? query.OrderByDescending(i => i.Name) : query.OrderBy(i => i.Name);
                    break;
                case "Country":
                    query = sortingData.SortOrder == "desc" ? query.OrderByDescending(i => i.Country) : query.OrderBy(i => i.Country);
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

            var result = await Task.Run(() => query.ToListAsync());
            return _mapper.Map<List<POCO.VehicleMake>>(result);
        }
        public async Task<int> GetMakeCount(string searchValue)
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
    }
}
