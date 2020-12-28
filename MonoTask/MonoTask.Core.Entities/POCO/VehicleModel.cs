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
        public int Year { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
