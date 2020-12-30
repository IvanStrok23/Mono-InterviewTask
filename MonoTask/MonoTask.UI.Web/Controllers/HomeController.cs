using MonoTask.Core.Services;
using MonoTask.UI.Web.Helper;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace MonoTask.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVehicleService _vehicleService;
        public HomeController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
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
                int id = await _vehicleService.InsertMake(makeItem);
                foreach (var modelItem in testData.GetHardCodedModelsByMakeName(makeItem.Name,id))
                {
                    _vehicleService.InsertModel(modelItem);
                }
            }
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { success = true, message = "Insert success!" }, JsonRequestBehavior.AllowGet);
        }

    }
}