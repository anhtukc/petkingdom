using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetKingdomFN.BusEntities;
using PetKingdomFN.Interfaces;
using PetKingdomFN.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage;

namespace PetKingdomFN.Controllers
{
    [Route("api/[controller]")]
    public class PetServiceOptionController : Controller
    {
        private readonly IPetServiceOptions _repo;
        private readonly IPetServiceRepository _petService;
        private readonly IScheduleAvailableRepository _scheduleAvailableRepos;
        private readonly IServiceSellPriceRepository _sellPriceRepos;

        public PetServiceOptionController(IPetServiceOptions repo, IPetServiceRepository petService, IScheduleAvailableRepository scheduleAvailableRepos, IServiceSellPriceRepository sellPriceRepos)
        {
            this._repo = repo;
            this._petService = petService;
            _sellPriceRepos = sellPriceRepos;
            _scheduleAvailableRepos = scheduleAvailableRepos;
        }
        [HttpGet("getall")]
        [AllowAnonymous]
        public async Task<JsonResult> GetAllByWeight(double petWeight)
        {
            try
            {
                List<ServiceOption> list = await _repo.GetAllPetServiceOptions(petWeight);
                return Json(new
                {
                    list = list,
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

        [HttpPost("getPage")]
        [Authorize]
        public async Task<JsonResult> Index([FromBody] Pagination page)
        {
            try
            {
                DataList<ServiceOption> result = await _repo.GetPageList(page);
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
        [DisableRequestSizeLimit]
        [Authorize]
        public async Task<JsonResult> AddPetServiceOption([FromForm] ServiceOption service)
        {
            try
            {
                var obj = await _repo.AddPetServiceOption(service);

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
        [HttpGet("getById")]
        [Authorize]
        public async Task<JsonResult> GetPetServiceOptionById([FromQuery] string id)
        {
            try
            {
                var obj = await _repo.GetPetServiceOptionById(id);
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
        [HttpGet("getByPetServiceId")]
        [AllowAnonymous]
        public async Task<JsonResult> GetByPetServiceId(string id)
        {
            try
            {
                var option = await _repo.GetPetServiceOptionByParentId(id);
                var jsonOptions = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };
                return Json(new { list = option, status = 1 });
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
        public async Task<JsonResult> UpdatePetServiceOption([FromForm] ServiceOption service)
        {
            try
            {
                var obj = await _repo.UpdatePetServiceOption(service);

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
        public async Task<JsonResult> DeletePetServiceOption([FromForm] string id)
        {
            try
            {
                var obj = await _repo.DeletePetServiceOption(id);
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
        public async Task<JsonResult> SearchPetServiceOption([FromBody] postingObject pObject)
        {
            try
            {
                DataList<ServiceOption> result = await _repo.SearchPetServiceOption(pObject.page, pObject.searchObj);
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
