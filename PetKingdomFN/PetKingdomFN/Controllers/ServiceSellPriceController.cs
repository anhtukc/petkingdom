using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetKingdomFN.BusEntities;
using PetKingdomFN.Interfaces;
using PetKingdomFN.Models;
using PetKingdomFN.Repositories;

namespace PetKingdomFN.Controllers
{
    [Route("api/[controller]")]
    public class ServiceSellPricesController : Controller
    {
        private readonly IServiceSellPriceRepository _repo;


        public ServiceSellPricesController(IServiceSellPriceRepository repo)
        {
            this._repo = repo;
        }

        [HttpPost("getPage")]
        [Authorize]
        public async Task<JsonResult> Index([FromBody] postingObjectWithId pObj)
        {
            try
            {
                DataList<ServiceSellPrice> result = await _repo.GetPageList(pObj.page, pObj.Id);
                return Json(new
                {
                    list = result.list,
                    numberOfRecords = result.numberOfRecords,
                    status = 1
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = 0,
                    details = ex.Message
                });
            }
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<JsonResult> AddServiceSellPrice([FromForm] ServiceSellPrice acc)
        {
            try
            {

                ServiceSellPrice obj = await _repo.AddServiceSellPrice(acc);

                return Json(new { obj = obj, status = 1 });
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, details = ex.Message });
            }

        }
        [HttpGet("getById")]
        [Authorize]
        public async Task<JsonResult> GetServiceSellPriceById([FromQuery] string id)
        {
            try
            {
                var obj = await _repo.GetServiceSellPriceById(id);
                return Json(new { obj = obj, status = 1 });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = 0,
                    details = ex.Message
                });
            }
        }
        [HttpGet("getByOptionId")]
        [AllowAnonymous]
        public async Task<JsonResult> GetServiceSellPriceByOptionId( string id)
        {
            try
            {
                var list = await _repo.GetServiceSellPriceByOptionId(id);
                return Json(new { list = list, status = 1 });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = 0,
                    details = ex.Message
                });
            }
        }

        [HttpPost("update")]
        [Authorize]
        public async Task<JsonResult> UpdateServiceSellPrice([FromForm] ServiceSellPrice acc)
        {
            try
            {
                var obj = await _repo.UpdateServiceSellPrice(acc);
                return Json(new { obj = obj, status = 1 });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = 0,
                    details = ex.Message
                });
            }
        }
        [HttpPost("delete")]
        [Authorize]
        public async Task<JsonResult> DeleteServiceSellPrice([FromForm] string id)
        {
            try
            {
                var obj = await _repo.DeleteServiceSellPrice(id);
                if (obj == 0)
                {
                    return Json(new { status = 0, details = "not found" });
                }
                return Json(new { status = 1 });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = 0,
                    details = ex.Message
                });
            }
        }

        [HttpPost("search")]
        [Authorize]
        public async Task<JsonResult> SearchServiceSellPrice([FromBody] postingObject pObject)
        {
            try
            {
                DataList<ServiceSellPrice> result = await _repo.SearchServiceSellPrice(pObject.page, pObject.searchObj);
                return Json(new
                {
                    list = result.list,
                    numberOfRecords = result.numberOfRecords,
                    status = 1
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, details = ex.Message });
            }
        }
    }
}
