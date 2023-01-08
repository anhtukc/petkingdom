using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetKingdomFN.BusEntities;
using PetKingdomFN.Interfaces;
using PetKingdomFN.Models;

namespace PetKingdomFN.Controllers
{
    [Route("api/[controller]")]
    public class SchedulesController : Controller
    {

        private readonly IScheduleRepository _repo;


        public SchedulesController(IScheduleRepository repo)
        {
            this._repo = repo;
        }

        [HttpPost("getPage")]
        [Authorize]
        public async Task<JsonResult> Index([FromBody] Pagination page)
        {
            try
            {
                DataList<Schedule> result = await _repo.GetPageList(page);
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
        public async Task<JsonResult> AddSchedule([FromBody] Schedule[] schedule)
        {
            try
            {

                Schedule[]  list = await _repo.AddSchedule(schedule);

                return Json(new { list = list, status = 1 });
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, details = ex.Message });
            }

        }
        [HttpGet("getById")]
        [Authorize]
        public async Task<JsonResult> GetScheduleById([FromQuery] string id)
        {
            try
            {
                var obj = await _repo.GetScheduleById(id);
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

        [HttpPost("update")]
        [Authorize]
        public async Task<JsonResult> UpdateSchedule([FromForm] Schedule schedule)
        {
            try
            {
                var obj = await _repo.UpdateSchedule(schedule);
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
        public async Task<JsonResult> DeleteSchedule([FromForm] string id)
        {
            try
            {
                var obj = await _repo.DeleteSchedule(id);
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
        public async Task<JsonResult> SearchSchedule([FromBody] postingObject pObject)
        {
            try
            {
                DataList<Schedule> result = await _repo.SearchSchedule(pObject.page, pObject.searchObj);
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
