using AutoMapper;
using MonoTask.Common.Interfaces.ServiceInterfaces;
using MonoTask.Core.Entities.Extensions;
using MonoTask.Core.Entities.Helpers;
using MonoTask.UI.Web.Helper;
using MonoTask.UI.Web.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using POCO = MonoTask.Core.Entities;

namespace MonoTask.UI.Web.Controllers
{
    public class MakeAdministrationController : Controller
    {
        private readonly IVehicleMakeService _vehicleMakeService;
        private readonly IMapper _mapper;

        public MakeAdministrationController(IVehicleMakeService vehicleMakeService, IMapper mapper)
        {
            _vehicleMakeService = vehicleMakeService;
            _mapper = mapper;

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
            SortParams param = new SortParams(sortingData.SortBy.ToSortByEnum(), sortingData.SortOrder.ToSortOrderEnum(), sortingData.SearchValue);


            var items = await _vehicleMakeService.GetMakesSortedByColumn(param);
            var count = await _vehicleMakeService.GetMakeCount(param.SearchValue);
            List<VehicleMakeView> viewItems = _mapper.Map<List<VehicleMakeView>>(items);
            ViewBag.Items = viewItems;
            setViewBagFilterData(sortingData, count);
            return View("Index");
        }
        public async Task<ActionResult> GetByPage(TableFilterData sortingData)
        {
            PagingParams param = new PagingParams(sortingData.Page, sortingData.SortBy.ToSortByEnum(), sortingData.SortOrder.ToSortOrderEnum(), sortingData.SearchValue);


            var items = await _vehicleMakeService.GetMakesByPage(param);
            var count = await _vehicleMakeService.GetMakeCount(param.SortParams.SearchValue);
            List<VehicleMakeView> viewItems = _mapper.Map<List<VehicleMakeView>>(items);
            ViewBag.Items = viewItems;
            setViewBagFilterData(sortingData, count);
            return View("Index");
        }

        public async Task<ActionResult> SearchByName(string searchValue)
        {
            searchValue = searchValue == null ? "" : searchValue;
            var items = await _vehicleMakeService.GetMakesByName(searchValue);
            var count = await _vehicleMakeService.GetMakeCount(searchValue);
            List<VehicleMakeView> viewItems = _mapper.Map<List<VehicleMakeView>>(items);
            ViewBag.Items = viewItems;
            setViewBagFilterData(new TableFilterData(searchValue), count);
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