using MonoTask.Infrastructure.DAL.Interfaces;
using System;

namespace MonoTask.Infrastructure.DAL.Entities
{
    public class VehicleModelEntity : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual VehicleMakeEntity VehiceMake { get; set; }

        public int GetId()
        {
           return Id;
        }
    }
}
