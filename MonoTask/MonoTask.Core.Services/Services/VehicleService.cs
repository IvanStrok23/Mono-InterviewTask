using AutoMapper;
using MonoTask.Infrastructure.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using POCO = MonoTask.Core.Entities;


namespace MonoTask.Core.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehiclesDbContext _vehiclesDbContext;
        private readonly IMapper _mapper;
        public VehicleService(IVehiclesDbContext vehiclesDbContext,IMapper mapper)
        {
            _vehiclesDbContext = vehiclesDbContext;
            _mapper = mapper;
        }

        #region VehicleModel

        public async Task<int> InsertModel(POCO.VehicleModel entity)
        {
            if (entity == null || entity.Id != 0)
            {
                return 0;
            }
            VehicleModel mapped = _mapper.Map<VehicleModel>(entity);
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
                        select new POCO.VehicleModel() {
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
        public async Task<List<POCO.VehicleModel>> GetModels(int page, string sortBy, string sortOrder, string searchValue)
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

            if (!String.IsNullOrWhiteSpace(searchValue))
            {
                query = query.Where(i => i.Name.StartsWith(searchValue));
            }

            query = query.Skip((page - 1) * 10).Take(10);
            var result = await Task.Run(() => query.ToListAsync()); //TODO:  Clean this
            return _mapper.Map<List<POCO.VehicleModel>>(result);
        }
        public async Task<int> GetModelCount(string searchValue)
        {
            return await Task.Run(() => _vehiclesDbContext.VehiclesModel.Where(i => i.Name.StartsWith(searchValue)).Count());
        }
        public async Task<bool> EditModel(POCO.VehicleModel model)
        {
            VehicleModel temp = _vehiclesDbContext.VehiclesModel.Where(i => i.Id == model.Id).FirstOrDefault();
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
            var m = await Task.Run(() =>  _vehiclesDbContext.VehiclesModel.FindAsync(id));
            if(m == null)
            {
                return false;
            }
            return await _vehiclesDbContext.Remove(m);
        }


        #endregion

        #region VehicleMake
        public async Task<int> InsertMake(POCO.VehicleMake entity)
        {
            if (entity == null || entity.Id != 0)
            {
                return 0;
            }
            VehicleMake mapped = _mapper.Map<VehicleMake>(entity);
            mapped.CreatedAt = DateTime.UtcNow;
            mapped.UpdatedAt = DateTime.UtcNow;
            return await _vehiclesDbContext.Insert(mapped);
        }

        public async Task<POCO.VehicleMake> GetMakeById(int id)
        {
            var result = await Task.Run(() => _vehiclesDbContext.Get<VehicleMake>(id));
            return _mapper.Map<POCO.VehicleMake>(result);
        }
        public async Task<List<POCO.VehicleMake>> GetMakes(int page, string sortBy, string sortOrder, string searchValue)
        {
            var query = _vehiclesDbContext.VehiclesMake.Where(i => i.Id != null);

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

            if (!String.IsNullOrWhiteSpace(searchValue))
            {
                query = query.Where(i => i.Name.StartsWith(searchValue));
            }
            query = query.Skip((page - 1) * 10).Take(10);

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
            VehicleMake temp = _vehiclesDbContext.VehiclesMake.Where(i => i.Id == model.Id).FirstOrDefault();
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

        #endregion
    }
}
