using MonoTask.Core.Services;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace MonoTask.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVehicleService _vehicleModelService;
        public HomeController(IVehicleService vehicleModelService)
        {
            _vehicleModelService = vehicleModelService;
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}