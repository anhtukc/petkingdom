using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetKingdomFN.Interfaces;
using PetKingdomFN.Repositories;
using PetKingdomFN.BusEntities;
using PetKingdomFN.Models;
using Microsoft.AspNetCore.Authorization;

namespace PetKingdomFN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetServiceController : Controller
    {
        private readonly IPetServiceRepository _PetServiceRepository;
        private readonly ICloudStorageService _cloud;

        public PetServiceController(IPetServiceRepository repo, ICloudStorageService cloud)
        {
            this._PetServiceRepository = repo;
            _cloud = cloud;
        }

        [HttpPost("GetPage")]
        public async Task<JsonResult> Index([FromBody]Pagination page)
        {
            IEnumerable<PetService> list = await _PetServiceRepository.GetPageList(page);
            int numberOfRecords = await _PetServiceRepository.GetNumberOfRecords();
            return Json(new { list, numberOfRecords });
        }

        [HttpPost("Add")]
        public async Task<JsonResult> AddPetService([FromBody]PetService service)
        {
            if (service is null)
            {
                return Json(new { message = "fail", details = "Empty object" });
            }
           
            await _PetServiceRepository.AddPetService(service);

            return Json(new { result = service , message = "fail" });
        }
        [HttpGet("getById")]
        public async Task<JsonResult> GetPetServiceById([FromQuery] string id)
        {
            var obj = await _PetServiceRepository.GetPetServiceById(id);
            if (obj is null)
            {
                return Json(new { message = "fail", details = "Not found" });
            }
            return Json(new { result = obj, message = "success" });
        }

        [HttpPost("update")]
        public async Task<JsonResult> UpdatePetService([FromBody] PetService service)
        {
            var obj = await _PetServiceRepository.UpdatePetService(service);
            if (obj is null)
            {
                return Json(new { message = "fail", details = "Not found" });
            }
            return Json(new { result = obj, message = "success" });
        }
        [HttpPost("delete")]
        public async Task<JsonResult> DeletePetService([FromBody] string  id)
        {
            var obj = await _PetServiceRepository.DeletePetService(id);
            if (obj == "Not found")
            {
                return Json(new { message = "fail", details = "Not found" });
            }
            return Json(new { message = "success" });
        }
    }
}
