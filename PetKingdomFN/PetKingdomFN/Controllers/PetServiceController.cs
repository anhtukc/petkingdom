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
using Google.Api.Gax.ResourceNames;

namespace PetKingdomFN.Controllers
{
    [Route("api/[controller]")]
    public class PetServiceController : Controller
    {
        private readonly IPetServiceRepository _PetServiceRepository;
        private readonly ICloudStorageService _cloud;
        private readonly string folder = "img/";

        public PetServiceController(IPetServiceRepository repo, ICloudStorageService cloud)
        {
            this._PetServiceRepository = repo;
            _cloud = cloud;
        }

        [HttpGet("getAll")]
        [AllowAnonymous]
        public async Task<JsonResult> GetAll()
        {
            try
            {
                List<PetService> list = await _PetServiceRepository.GetAll();
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
                DataList<PetService> result = await _PetServiceRepository.GetPageList(page);
                return Json(new
                {
                    list = result.list,
                    numberOfRecords = result.numberOfRecords,
                    status  = 1
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                   status  = 0,
                    details = ex.Message
                });
            }
        }

        [HttpPost("add")]
        [Authorize]
        [DisableRequestSizeLimit]
        public async Task<JsonResult> AddPetService([FromForm] PetService service)
        {
            try
            {
                if (!(service.iconFile is null))
                {
                    service.Icon = await _cloud.UploadFileAsync(service.iconFile, folder + service.Id);
                }
                PetService obj = await _PetServiceRepository.AddPetService(service);

                return Json(new { obj = obj, status  = 1 });
            }
            catch (Exception ex)
            {
                return Json(new {status  = 0, details = ex.Message });
            }

        }
        [HttpGet("getById")]
        [Authorize]
        public async Task<JsonResult> GetPetServiceById([FromQuery] string id)
        {
            try
            {
                var obj = await _PetServiceRepository.GetPetServiceById(id);              
                return Json(new { obj = obj, status  = 1 });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                   status  = 0,
                    details = ex.Message
                });
            }
        }

        [HttpPost("update")]
        [Authorize]
        public async Task<JsonResult> UpdatePetService([FromForm] PetService service)
        {
            try
            {
                if (!(service.iconFile is null))
                {
                    service.Icon = await _cloud.UploadFileAsync(service.iconFile, folder + service.Id);
                }
                var obj = await _PetServiceRepository.UpdatePetService(service);
                return Json(new { obj = obj, status  = 1 });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                   status  = 0,
                    details = ex.Message
                });
            }
        }
        [HttpPost("delete")]
        [Authorize]
        public async Task<JsonResult> DeletePetService([FromForm] string id)
        {
            try
            {
                var obj = await _PetServiceRepository.DeletePetService(id);
                if(obj == 0)
                {
                    return Json(new { status = 0 ,details="not found"});
                }
                return Json(new { status  = 1 });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                   status  = 0,
                    details = ex.Message
                });
            }
        }

        [HttpPost("search")]
        [Authorize]
        public async Task<JsonResult> SearchPetService([FromBody] postingObject pObject)
        {
            try
            {
                DataList<PetService> result = await _PetServiceRepository.SearchPetService(pObject.page, pObject.searchObj);
                return Json(new
                {
                    list = result.list,
                    numberOfRecords = result.numberOfRecords,
                    status  = 1
                });
            }
            catch (Exception ex)
            {
                return Json(new {status  = 0, details = ex.Message });
            }
        }
    }
}
