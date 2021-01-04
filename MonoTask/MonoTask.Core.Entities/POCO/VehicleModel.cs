
using System;
using System.Collections.Generic;

namespace MonoTask.Core.Entities
{
    public class VehicleModel 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MakeId { get; set; }
        public string MakeName { get; set; }
        public int Year { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; } //TOCHECK: Do we need this in viewmodel ?

    }
}
