using MonoTask.Core.Services;
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
            var makeDropdown = await _vehicleService.GetMakeDropdown(); //TODO: Will pull data after every refresh - make it just on open edit or add
            ViewBag.MakeDropdownList = new SelectList(makeDropdown, "Key", "Value", 1);
            return View();
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

        public async Task<ActionResult> Insert(POCO.VehicleModel model)
        {
            bool response = await _vehicleService.InsertModel(model);
            if (response)
            {
                return getResult(HttpStatusCode.OK, "Insert success!");
            }
            return getResult(HttpStatusCode.BadRequest, "BadRequest");
        }
        public async Task<ActionResult> GetMakeDropdown()
        {
            //Info: call to make page dropdown in order to make it with limit
            var makeDropdown = await _vehicleService.GetMakeDropdown();
            ViewBag.MakeDropdownList = new SelectList(makeDropdown, "Key", "Value", 1);
            return getResult(HttpStatusCode.OK, "Success!");
        }

        public async Task<ActionResult> Edit(POCO.VehicleModel model)
        {
            bool response = await _vehicleService.EditModel(model);
            if (response)
            {
                return getResult(HttpStatusCode.OK, "Edit success!");
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