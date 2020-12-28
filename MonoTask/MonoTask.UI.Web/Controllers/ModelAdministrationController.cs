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
        private readonly IVehicleService _vehicleModelService;
        private List<POCO.VehicleMake> listedData = new List<POCO.VehicleMake>(); // Not sure about this
        public ModelAdministrationController(IVehicleService vehicleModelService)
        {
            _vehicleModelService = vehicleModelService;
        }
        public async Task<ActionResult> Index(int page = 1,string sortBy = "Name",string sortOrder = "asc",string searchValue = "")
        {

            page = page <= 0 ? 1 : page;

            var items = await _vehicleModelService.GetModels(page, sortBy, sortOrder, searchValue);
            var count = await _vehicleModelService.GetCount(searchValue);
            ViewBag.Items = items;
            ViewBag.SortOrder = sortOrder;
            ViewBag.SearchValue = searchValue;
            ViewBag.CurrentPage = page;
            ViewBag.PageMax = count / 10;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            bool response = await _vehicleModelService.DeleteModel(id);
            if (response)
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(new { success = true, message = "Deletion success!" }, JsonRequestBehavior.AllowGet);
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { success = false, message = "BadRequest" }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Insert(POCO.VehicleModel model)
        {
            bool response = await _vehicleModelService.Insert(model);
            if (response)
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(new { success = true, message = "Insert success!" }, JsonRequestBehavior.AllowGet);
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { success = false, message = "BadRequest" }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Edit(POCO.VehicleModel model)
        {
            bool response = await _vehicleModelService.EditModel(model);
            if (response)
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(new { success = true, message = "Edit success!" }, JsonRequestBehavior.AllowGet);
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { success = false, message = "BadRequest" }, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public async Task<ActionResult> Sort(string sortOrder)
        //{
        //    //var items = await _vehicleModelService.GetModels(_page, "Name", _sortOrder, "");
        //    //if (items.Count == 0)
        //    //{
        //    //    ViewBag.Message = "No Data";
        //    //}
        //    //else
        //    //{
        //    //    ViewBag.Items = items;
        //    //}

        //    //ViewBag.SortOrder = _sortOrder;
        //    //_sortOrder = sortOrder;
        //    Response.StatusCode = (int)HttpStatusCode.OK;
        //    return Json(new { success = true, message = "" }, JsonRequestBehavior.AllowGet);
        //}
    }
}