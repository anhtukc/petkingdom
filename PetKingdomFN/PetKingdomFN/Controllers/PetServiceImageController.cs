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
        [HttpPost("getall")]
        public async Task<JsonResult> Index([FromForm] string serviceId)
        {
            try
            {
                IEnumerable<ServiceImage> list = await _repo.GetAllById(serviceId);
                return Json(new { list, message = "success" });
            }
            catch(Exception ex) {
                return Json(new {  message = "fail", details=ex.Message });
            }
        }
        [HttpPost("add")]
        [DisableRequestSizeLimit]
        public async Task<JsonResult> AddPetServiceImage([FromForm] List<IFormFile> files, [FromForm] string serviceId)
        {
            try
            {
                if (files is null)
                {
                    return Json(new { message = "fail", details = "Empty object" });
                }
                List<ServiceImage> list = await _repo.UploadImage(files, serviceId);
                return Json(new { list = list, message = "success" });
            }
            catch(Exception ex)
            {
                return Json(new { message = "fail", details = ex.Message });
            }
        }
        [HttpPost("delete")]
        public async Task<JsonResult> DeleteServiceImage([FromForm]string id)
        {
            try
            {
                if (id is null)
                {
                    return Json(new { message = "fail", details = "Empty object" });
                }
                await _repo.DeleteImage(id);
                return Json(new { message = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { message = "fail", details = ex.Message });
            }
        }
    }
}
