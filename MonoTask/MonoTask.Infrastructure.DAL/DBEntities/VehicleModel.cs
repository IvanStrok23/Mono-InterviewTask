using MonoTask.Infrastructure.DAL.Interfaces;
using System;

namespace MonoTask.Infrastructure.DAL.Entities
{
    public class VehicleModel : IEntity
    {
        public int Id { get; set; }
        public int MakeId { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int GetId()
        {
           return Id;
        }
    }
}
