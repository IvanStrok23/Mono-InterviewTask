using MonoTask.Core.Services;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using POCO = MonoTask.Core.Entities;

namespace MonoTask.UI.Web.Controllers
{
    public class MakeAdministrationController : Controller
    {
        private readonly IVehicleService _vehicleService;
        public MakeAdministrationController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<ActionResult> Index(int page = 1, string sortBy = "Name", string sortOrder = "asc", string searchValue = "")
        {

            page = page <= 0 ? 1 : page;
            var items = await _vehicleService.GetMakes(page, sortBy, sortOrder, searchValue);
            var count = await _vehicleService.GetMakeCount(searchValue);
            ViewBag.Items = items;
            ViewBag.SortOrder = sortOrder;
            ViewBag.SearchValue = searchValue;
            ViewBag.CurrentPage = page;
            ViewBag.PageMax = count / 10 + (count % 10 == 0 ? 0 : 1);
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Insert(POCO.VehicleMake model)
        {
            int id = await _vehicleService.InsertMake(model);
            if (id != 0)
            {
                return getResult(HttpStatusCode.OK, "Insert success!");
            }
            return getResult(HttpStatusCode.BadRequest, "BadRequest");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(POCO.VehicleMake model)
        {
            bool response = await _vehicleService.EditMake(model);
            if (response)
            {
                return getResult(HttpStatusCode.OK, "Edit success!");
            }
            return getResult(HttpStatusCode.BadRequest, "BadRequest");
        }


        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            bool response = await _vehicleService.DeleteMake(id);
            if (response)
            {
                return getResult(HttpStatusCode.OK, "Deletion success!");
            }
            return getResult(HttpStatusCode.BadRequest, "BadRequest");
        }

       
        private JsonResult getResult(HttpStatusCode code, string text)
        {
            Response.StatusCode = (int)code;
            return Json(new { success = false, message = text }, JsonRequestBehavior.AllowGet);
        }
    }
}