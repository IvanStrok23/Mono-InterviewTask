using MonoTask.Common.Interfaces.ServiceInterfaces;
using MonoTask.UI.Web.Helper;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace MonoTask.UI.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IVehicleModelService _vehicleModelService;
        private readonly IVehicleMakeService _vehicleMakeService;

        public HomeController(IVehicleModelService vehicleModelService, IVehicleMakeService vehicleMakeService)
        {
            _vehicleModelService = vehicleModelService;
            _vehicleMakeService = vehicleMakeService;
        }
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> InsertTestData()
        {
            TestData testData = new TestData();
            foreach (var makeItem in testData.MakeList)
            {
                int id = await _vehicleMakeService.InsertMake(makeItem);
                foreach (var modelItem in testData.GetHardCodedModelsByMakeName(makeItem.Name,id))
                {
                   await _vehicleModelService.InsertModel(modelItem);
                }
            }
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { success = true, message = "Insert success!" }, JsonRequestBehavior.AllowGet);
        }

    }
}