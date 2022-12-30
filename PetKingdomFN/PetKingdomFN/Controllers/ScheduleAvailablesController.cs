using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using PetKingdomFN.BusEntities;
using PetKingdomFN.Interfaces;
using PetKingdomFN.Models;

namespace PetKingdomFN.Controllers
{
    [Route("api/[controller]")]
    public class ScheduleAvailablesController : Controller
    {

        private readonly IScheduleAvailableRepository _repo;


        public ScheduleAvailablesController(IScheduleAvailableRepository repo)
        {
            this._repo = repo;
        }

        [HttpPost("getPage")]
        [Authorize]
        public async Task<JsonResult> Index([FromBody] postingObjectWithId pObj)
        {
            try
            {
                DataList<ScheduleAvailable> result = await _repo.GetPageList(pObj.page, pObj.Id);
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
        [HttpPost("search")]
        [Authorize]
        public async Task<JsonResult> SearchScheduleAvailable([FromBody] postingObject pObject, string optionId)
        {
            try
            {
                DataList<ScheduleAvailable> result = await _repo.SearchScheduleAvailable(pObject.page, pObject.searchObj, optionId);
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
        [HttpPost("add")]
        [Authorize]
        public async Task<JsonResult> AddScheduleAvailable([FromForm] ScheduleAvailable sa)
        {
            try
            {

                ScheduleAvailable obj = await _repo.AddScheduleAvailable(sa);

                return Json(new { obj = obj, status = 1 });
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, details = ex.Message });
            }

        }
      
        [HttpGet("getById")]
        [Authorize]
        public async Task<JsonResult> GetScheduleAvailableById([FromQuery] string id)
        {
            try
            {
                var obj = await _repo.GetScheduleAvailableById(id);
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
        public async Task<JsonResult> GetScheduleAvailableByOptionId([FromQuery] string id, [FromQuery] string startedDateFormat)
        {
            try
            {
                var list = await _repo.GetScheduleAvailableByOptionId(id,startedDateFormat);
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
        public async Task<JsonResult> UpdateScheduleAvailable([FromForm] ScheduleAvailable sa)
        {
            try
            {
                var obj = await _repo.UpdateScheduleAvailable(sa);
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
        public async Task<JsonResult> DeleteScheduleAvailable([FromForm] string id)
        {
            try
            {
                var obj = await _repo.DeleteScheduleAvailable(id);
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

        
    }
}
