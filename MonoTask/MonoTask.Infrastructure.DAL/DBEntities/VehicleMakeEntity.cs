using MonoTask.Infrastructure.DAL.Interfaces;
using System;

namespace MonoTask.Infrastructure.DAL.Entities
{
    public class VehicleMakeEntity : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int GetId()
        {
            return Id;
        }
    }
}
