using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetKingdomFN.BusEntities;
using PetKingdomFN.Helpers;
using PetKingdomFN.Interfaces;
using PetKingdomFN.Models;
using static NuGet.Packaging.PackagingConstants;

namespace PetKingdomFN.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {

        private readonly ICustomerRepository _repo;
        private readonly IAccountRepository _accRepo;
        private readonly ICloudStorageService _cloud;
        private readonly string folder = "img/customer";
        public CustomersController(ICustomerRepository repo, ICloudStorageService cloud, IAccountRepository accRepo)
        {
            this._repo = repo;
            _cloud = cloud;
            _accRepo = accRepo;
        }

        [HttpPost("getPage")]
        [Authorize]
        public async Task<JsonResult> Index([FromBody] Pagination page)
        {
            try
            {
                DataList<Customer> result = await _repo.GetPageList(page);
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
        public async Task<JsonResult> AddCustomer([FromBody] CustomerAccount ca)
        {
            try
            {
                string checkacc = await _accRepo.CheckCustomerAccount(ca.acc.Username, ca.cus.Phonenumber, ca.cus.Email);
                if(checkacc == "duplicate") {
                    return Json(new { status = 0, details = "duplicate" });
                }

                Customer obj = await _repo.AddCustomer(ca.cus, ca.acc);

                return Json(new { obj = obj, status = 1 });
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, details = ex.Message });
            }

        }
        [HttpGet("getById")]
        [Authorize]
        public async Task<JsonResult> GetCustomerById([FromQuery] string id)
        {
            try
            {

                var obj = await _repo.GetCustomerById(id);
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
        public async Task<JsonResult> UpdateCustomer([FromForm] Customer cus)
        {
            try
            {
               
                var obj = await _repo.UpdateCustomer(cus);
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
        public async Task<JsonResult> DeleteCustomer([FromForm] string id)
        {
            try
            {
                var obj = await _repo.DeleteCustomer(id);
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
        public async Task<JsonResult> SearchCustomer([FromBody] postingObject pObject)
        {
            try
            {
                DataList<Customer> result = await _repo.SearchCustomer(pObject.page, pObject.searchObj);
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
