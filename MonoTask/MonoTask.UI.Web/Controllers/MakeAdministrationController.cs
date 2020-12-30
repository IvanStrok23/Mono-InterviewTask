using MonoTask.Core.Services;
using MonoTask.Core.Services.Interfaces;
using MonoTask.UI.Web.Helper;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using POCO = MonoTask.Core.Entities;

namespace MonoTask.UI.Web.Controllers
{
    public class MakeAdministrationController : Controller
    {
        private readonly IVehicleMakeService _vehicleMakeService;
        public MakeAdministrationController(IVehicleMakeService vehicleMakeService)
        {
            _vehicleMakeService = vehicleMakeService;
        }

        public async Task<ActionResult> Index(SortingData sortingData)
        {
            sortingData = sortingData == null ? new SortingData() : sortingData;

            sortingData.Page = sortingData.Page <= 0 ? 1 : sortingData.Page;
            var items = await _vehicleMakeService.GetMakes(sortingData);
            var count = await _vehicleMakeService.GetMakeCount(sortingData.SearchValue);
            ViewBag.Items = items;
            ViewBag.SortOrder = sortingData.SortOrder;
            ViewBag.SearchValue = sortingData.SearchValue;
            ViewBag.CurrentPage = sortingData.Page;
            ViewBag.PageMax = count / 10 + (count % 10 == 0 ? 0 : 1);
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Insert(POCO.VehicleMake model)
        {
            int id = await _vehicleMakeService.InsertMake(model);
            if (id != 0)
            {
                return getResult(HttpStatusCode.OK, "Insert success!");
            }
            return getResult(HttpStatusCode.BadRequest, "BadRequest");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(POCO.VehicleMake model)
        {
            bool response = await _vehicleMakeService.EditMake(model);
            if (response)
            {
                return getResult(HttpStatusCode.OK, "Edit success!");
            }
            return getResult(HttpStatusCode.BadRequest, "BadRequest");
        }


        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            bool response = await _vehicleMakeService.DeleteMake(id);
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