using System;

namespace MonoTask.Infrastructure.DAL.Entities
{
    public class VehicleMake
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
