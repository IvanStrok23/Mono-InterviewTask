using MonoTask.Core.Services;
using MonoTask.Core.Services.Interfaces;
using MonoTask.UI.Web.Helper;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using POCO = MonoTask.Core.Entities;

namespace MonoTask.UI.Web.Controllers
{
    public class ModelAdministrationController : Controller
    {
        private readonly IVehicleModelService _vehicleModelService;
        private readonly IVehicleMakeService _vehicleMakeService;
        public ModelAdministrationController(IVehicleModelService vehicleModelService, IVehicleMakeService vehicleMakeService)
        {
            _vehicleModelService = vehicleModelService;
            _vehicleMakeService = vehicleMakeService;
        }
        public async Task<ActionResult> Index(SortingData sortingData)
        {
            sortingData = sortingData == null ? new SortingData() : sortingData;

            sortingData.Page = sortingData.Page <= 0 ? 1 : sortingData.Page;

            var items = await _vehicleModelService.GetModels(sortingData);
            var count = await _vehicleModelService.GetModelCount(sortingData.SearchValue);
            ViewBag.Items = items;
            ViewBag.SortOrder = sortingData.SortOrder;
            ViewBag.SearchValue = sortingData.SearchValue;
            ViewBag.CurrentPage = sortingData.Page;
            ViewBag.PageMax = count / 10 + (count % 10 == 0 ? 0 : 1) ;
            if (ViewBag.MakeDropdownList == null)
            {
                ViewBag.MakeDropdownList = new SelectList(new Dictionary<int, string>() { { 0, "" } }, "Key", "Value", 1);
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Insert(POCO.VehicleModel model)
        {
            int id = await _vehicleModelService.InsertModel(model);
            if (id != 0)
            {
                return getResult(HttpStatusCode.OK, "Insert success!");
            }
            return getResult(HttpStatusCode.BadRequest, "BadRequest");
        }

        [HttpPost]
        public async Task<ActionResult> GetMakeDropdown()
        {
            //TODO: Limit data
            var makeDropdown = await _vehicleMakeService.GetMakeDropdown();
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { success = false, message = "Success!",data = Newtonsoft.Json.JsonConvert.SerializeObject(makeDropdown) }, JsonRequestBehavior.AllowGet);

        }
        
        [HttpPost]
        public async Task<ActionResult> Edit(POCO.VehicleModel model)
        {
            bool response = await _vehicleModelService.EditModel(model);
            if (response)
            {
                return getResult(HttpStatusCode.OK, "Edit success!");
            }
            return getResult(HttpStatusCode.BadRequest, "BadRequest");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            bool response = await _vehicleModelService.DeleteModel(id);
            if (response)
            {
                return getResult(HttpStatusCode.OK, "Deletion success!");
            }
            return getResult(HttpStatusCode.BadRequest, "BadRequest");
        }

        private JsonResult getResult(HttpStatusCode code , string text)
        {
            Response.StatusCode = (int)code;
            return Json(new { success = false, message = text }, JsonRequestBehavior.AllowGet);
        }
    }
}