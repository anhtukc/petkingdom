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
    public class PetServiceImageController : Controller
    {
        private readonly IServiceImage _repo;
        private readonly ICloudStorageService _cloud;
        private readonly string folder = "img/services/";

        public PetServiceImageController(IServiceImage repo, ICloudStorageService cloud)
        {
            _repo = repo;
            _cloud = cloud;
        }
        [HttpGet("getall")]
        [AllowAnonymous]
        public async Task<JsonResult> Index(string serviceId)
        {
            try
            {
                IEnumerable<ServiceImage> list = await _repo.GetAllById(serviceId);
                return Json(new { list, status  = 1 });
            }
            catch(Exception ex) {
                return Json(new {  status  = 0, details=ex.Message });
            }
        }
        [HttpPost("add")]
        [DisableRequestSizeLimit]
        [Authorize]
        public async Task<JsonResult> AddPetServiceImage([FromForm] List<IFormFile> files, [FromForm] string serviceId)
        {
            try
            {
                if (files is null)
                {
                    return Json(new { status  = 0, details = "Empty object" });
                }
                List<ServiceImage> list = await _repo.UploadImage(files, serviceId);
                return Json(new { list = list, status  = 1 });
            }
            catch(Exception ex)
            {
                return Json(new { status  = 0, details = ex.Message });
            }
        }
        [HttpPost("delete")]
        [Authorize]
        public async Task<JsonResult> DeleteServiceImage([FromForm]string id)
        {
            try
            {
                if (id is null)
                {
                    return Json(new { status  = 0, details = "Empty object" });
                }
                await _repo.DeleteImage(id);
                return Json(new { status  = 1 });
            }
            catch (Exception ex)
            {
                return Json(new { status  = 0, details = ex.Message });
            }
        }
    }
}
