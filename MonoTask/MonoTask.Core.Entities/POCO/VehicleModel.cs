using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTask.Core.Entities
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MakeId { get; set; }
        public string MakeName { get; set; }
        public int Year { get; set; }
        public DateTime CreatedAt { get; set; } // Do we need this in viewmodel ?
        public DateTime UpdatedAt { get; set; }
    }
}
