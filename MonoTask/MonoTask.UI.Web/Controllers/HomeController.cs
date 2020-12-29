using MonoTask.Core.Services;
using MonoTask.Infrastructure.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using POCO = MonoTask.Core.Entities;


namespace MonoTask.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVehicleService _vehicleModelService;
        public HomeController(IVehicleService vehicleModelService)
        {
            _vehicleModelService = vehicleModelService;
        }

        public async Task<ActionResult> Index()
        {
            //for (int i = 0; i < 20; i++)
            //{
            //    VehicleModel item = new VehicleModel()
            //    {
            //        MakeId = i + 23,
            //        Name = "Auto" + i,
            //        Year = i + 1900,
            //        CreatedAt = DateTime.UtcNow,
            //        UpdatedAt = DateTime.UtcNow
            //    };
            //    await _vehicleModelService.Insert(item);
            //}


          // var item1 = await _vehicleModelService.GetModelById(206);
           var item1 = await _vehicleModelService.GetMakeById(2);
            //if (item1 == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            //}

           // ViewBag.Item = item1;
            return View();
        }

        public async Task<ActionResult> Delete(int id)
        {
            bool response = await _vehicleModelService.DeleteModel(id);
            if (response)
            {
                return View();
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}