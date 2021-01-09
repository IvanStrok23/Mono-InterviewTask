using MonoTask.Core.Entities.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MonoTask.Infrastructure.DAL.Entities
{
    public class VehicleMakeEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual ICollection<VehicleModelEntity> Models { get; set; }

        public int GetId()
        {
            return Id;
        }
    }
}
