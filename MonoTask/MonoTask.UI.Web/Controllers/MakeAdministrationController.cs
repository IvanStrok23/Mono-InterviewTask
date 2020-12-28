using MonoTask.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MonoTask.UI.Web.Controllers
{
    public class MakeAdministrationController : Controller
    {
        private readonly IVehicleService _vehicleModelService;
        public MakeAdministrationController(IVehicleService vehicleModelService)
        {
            _vehicleModelService = vehicleModelService;
        }
        public async Task<ActionResult> Index()
        {
            var item1 = await _vehicleModelService.GetModelById(4);
            if (item1 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            ViewBag.Item = item1;
            return View();
        }
    }
}