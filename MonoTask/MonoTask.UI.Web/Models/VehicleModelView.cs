using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonoTask.UI.Web.Models
{
    public class VehicleModelView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MakeId { get; set; }
        public string MakeName { get; set; }
        public int Year { get; set; }
    }
}