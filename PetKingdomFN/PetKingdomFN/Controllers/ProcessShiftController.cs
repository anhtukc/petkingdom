using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetKingdomFN.BusEntities;
using PetKingdomFN.Interfaces;
using PetKingdomFN.Models;
using PetKingdomFN.Repositories;
using System.Collections.Generic;

namespace PetKingdomFN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessShiftController : Controller
    {
        private readonly IProcessShiftRepository _repo;
        private readonly ICloudStorageService _cloud;
        private readonly string folder = "img/services/";

        public ProcessShiftController(IProcessShiftRepository repo, ICloudStorageService cloud)
        {
            _repo = repo;
            _cloud = cloud;
        }
        [HttpPost("getall")]
        [Authorize]
        public async Task<JsonResult> Index([FromForm] string shiftId)
        {
            try
            {
                IEnumerable<ProcessShift> list = await _repo.GetAllById(shiftId);
                return Json(new { list, status = 1 });
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, details = ex.Message });
            }
        }
        [HttpPost("add")]
        [DisableRequestSizeLimit]
        [Authorize]
        public async Task<JsonResult> AddProcessShift([FromForm] List<IFormFile> files, [FromForm] string shiftId)
        {
            try
            {
                if (files is null)
                {
                    return Json(new { status = 0, details = "Empty object" });
                }
                List<ProcessShift> list = await _repo.UploadImage(files, shiftId);
                return Json(new { list = list, status = 1 });
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, details = ex.Message });
            }
        }
        [HttpPost("delete")]
        [Authorize]
        public async Task<JsonResult> DeleteProcessShift([FromForm] string id)
        {
            try
            {
                if (id is null)
                {
                    return Json(new { status = 0, details = "Empty object" });
                }
                await _repo.DeleteImage(id);
                return Json(new { status = 1 });
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, details = ex.Message });
            }
        }
    }
}
