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

        public async Task<ActionResult> Index()
        {

            var items = await _vehicleMakeService.GetMakes();
            var count = await _vehicleMakeService.GetMakeCount();
            ViewBag.Items = items;
            ViewBag.SortOrder = "asc";
            ViewBag.CurrentPage = 1;
            ViewBag.PageMax = count / 10 + (count % 10 == 0 ? 0 : 1);
            return View();
        }

        public async Task<ActionResult> SoryByColumn(TableFilterData sortingData)
        {

            var items = await _vehicleMakeService.GetMakesSortedByColumn(sortingData);
            var count = await _vehicleMakeService.GetMakeCount(sortingData.SearchValue);
            ViewBag.Items = items;
            setViewBagFilterData(sortingData, count);
            return View("Index");
        }
        public async Task<ActionResult> GetByPage(TableFilterData sortingData)
        {
            sortingData.Page = sortingData.Page <= 0 ? 1 : sortingData.Page;
            var items = await _vehicleMakeService.GetMakesByPage(sortingData);
            var count = await _vehicleMakeService.GetMakeCount(sortingData.SearchValue);
            ViewBag.Items = items;
            setViewBagFilterData(sortingData, count);
            return View("Index");
        }

        public async Task<ActionResult> SearchByName(TableFilterData sortingData)
        {
            sortingData.SearchValue = sortingData.SearchValue == null ? "" : sortingData.SearchValue;
            var items = await _vehicleMakeService.GetMakesByName(sortingData);
            var count = await _vehicleMakeService.GetMakeCount(sortingData.SearchValue);
            ViewBag.Items = items;
            setViewBagFilterData(sortingData, count);
            return View("Index");
        }

        private void setViewBagFilterData(TableFilterData sortingData, int setCount)
        {
            ViewBag.SortOrder = sortingData.SortOrder;
            ViewBag.SearchValue = sortingData.SearchValue;
            ViewBag.CurrentPage = sortingData.Page;
            ViewBag.PageMax = setCount / 10 + (setCount % 10 == 0 ? 0 : 1);
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