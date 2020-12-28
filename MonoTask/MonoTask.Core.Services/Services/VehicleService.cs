using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using MonoTask.Infrastructure.DAL.AutoMapper;
using MonoTask.Infrastructure.DAL.Entities;
using MonoTask.Infrastructure.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
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

        //public async Task Update<T>(IEntity entity) where T : class
        //{
        //    await _vehiclesDbContext.Update(entity);
        //}

        //async Task IVehicleService.Insert<T>(T entity)
        //{
        //    await _vehiclesDbContext.Insert(entity);
        //}
        //async Task IVehicleService.Insert<T>(List<T> entities)
        //{
        //    await _vehiclesDbContext.Insert(entities);
        //}

        //public async Task InsertVehicleModel(IEntity entity)
        //{
        //    await _vehiclesDbContext.Insert(entity);
        //}

        public async Task<bool> Insert(POCO.VehicleModel entity)
        {
            if (entity == null || entity.Id != 0)
            {
                return false;
            }
            VehicleModel temp = new VehicleModel()
            {
                MakeId = entity.MakeId,
                Name = entity.Name,
                Year = entity.Year,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _vehiclesDbContext.Insert(temp);
            return true;
        }


        public async Task<POCO.VehicleModel> GetModelById(int id)
        {
            var result = await Task.Run(()=> _vehiclesDbContext.Get<VehicleModel>(id).Result);
            if(result == null)
            {
                return null;
            }
            return _mapper.Map<POCO.VehicleModel>(result);
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

        public async Task<List<POCO.VehicleModel>> GetModels(int page, string sortBy, string sortOrder, string searchValue)
        {
            var query = _vehiclesDbContext.VehiclesModel.Where(i => i.Id != null);

            switch (sortBy)
            {
                case "Name":
                    query = sortOrder == "desc" ? query.OrderByDescending(i => i.Name) : query.OrderBy(i => i.Name);
                    break;
                case "Make":
                    query = sortOrder == "desc" ? query.OrderByDescending(i => i.Year) : query.OrderBy(i => i.Year);
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
            var result = await Task.Run(() => query.ToListAsync());
            return _mapper.Map<List<POCO.VehicleModel>>(result);
        }

        public async Task<int> GetCount(string searchValue)
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
    }
}
