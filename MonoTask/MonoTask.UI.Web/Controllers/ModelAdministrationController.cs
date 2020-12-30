using MonoTask.Core.Services;
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
        private readonly IVehicleService _vehicleService;
        public ModelAdministrationController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }
        public async Task<ActionResult> Index(int page = 1,string sortBy = "Name",string sortOrder = "asc",string searchValue = "")
        {
            page = page <= 0 ? 1 : page;

            var items = await _vehicleService.GetModels(page, sortBy, sortOrder, searchValue);
            var count = await _vehicleService.GetModelCount(searchValue);
            ViewBag.Items = items;
            ViewBag.SortOrder = sortOrder;
            ViewBag.SearchValue = searchValue;
            ViewBag.CurrentPage = page;
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
            int id = await _vehicleService.InsertModel(model);
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
            var makeDropdown = await _vehicleService.GetMakeDropdown();
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { success = false, message = "Success!",data = Newtonsoft.Json.JsonConvert.SerializeObject(makeDropdown) }, JsonRequestBehavior.AllowGet);

        }
        
        [HttpPost]
        public async Task<ActionResult> Edit(POCO.VehicleModel model)
        {
            bool response = await _vehicleService.EditModel(model);
            if (response)
            {
                return getResult(HttpStatusCode.OK, "Edit success!");
            }
            return getResult(HttpStatusCode.BadRequest, "BadRequest");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            bool response = await _vehicleService.DeleteModel(id);
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