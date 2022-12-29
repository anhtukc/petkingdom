using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetKingdomFN.BusEntities;
using PetKingdomFN.Interfaces;
using PetKingdomFN.Models;

namespace PetKingdomFN.Controllers
{
    [Route("api/[controller]")]
    public class PetsController : Controller
    {

        private readonly IPetRepository _repo;


        public PetsController(IPetRepository repo)
        {
            this._repo = repo;
        }

        [HttpPost("getPage")]
        [Authorize]
        public async Task<JsonResult> Index([FromBody] Pagination page)
        {
            try
            {
                DataList<Pet> result = await _repo.GetPageList(page);
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
        public async Task<JsonResult> AddPet([FromForm] Pet acc)
        {
            try
            {

                Pet obj = await _repo.AddPet(acc);

                return Json(new { obj = obj, status = 1 });
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, details = ex.Message });
            }

        }
        [HttpGet("getById")]
        [Authorize]
        public async Task<JsonResult> GetPetById([FromQuery] string id)
        {
            try
            {
                var obj = await _repo.GetPetById(id);
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
        [HttpGet("getByCustomerId")]
        [Authorize]
        public async Task<JsonResult> GetByCustomerId([FromQuery] string CustomerId)
        {
            try
            {
                var list = await _repo.GetPetByCustomerId(CustomerId);
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
        public async Task<JsonResult> UpdatePet([FromForm] Pet acc)
        {
            try
            {
                var obj = await _repo.UpdatePet(acc);
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
        public async Task<JsonResult> DeletePet([FromForm] string id)
        {
            try
            {
                var obj = await _repo.DeletePet(id);
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
        public async Task<JsonResult> SearchPet([FromBody] postingObject pObject)
        {
            try
            {
                DataList<Pet> result = await _repo.SearchPet(pObject.page, pObject.searchObj);
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
