using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetKingdomFN.BusEntities;
using PetKingdomFN.Interfaces;
using PetKingdomFN.Models;

namespace PetKingdomFN.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        
        private readonly IAccountRepository _repo;


        public AccountsController(IAccountRepository repo)
        {
            this._repo = repo;
        }

        [HttpGet("getall")]
        [Authorize]
        public async Task<JsonResult> Index1(string permistion)
        {
            try
            {
                List<Account> list = await _repo.GetAll(permistion);
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
                DataList<Account> result = await _repo.GetPageList(page);
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
        public async Task<JsonResult> AddAccount([FromForm] Account acc)
        {
            try
            {
              
                Account obj = await _repo.AddAccount(acc);

                return Json(new { obj = obj, status = 1 });
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, details = ex.Message });
            }

        }
        [HttpGet("getById")]
        [Authorize]
        public async Task<JsonResult> GetAccountById([FromQuery] string id)
        {
            try
            {
                var obj = await _repo.GetAccountById(id);
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
        [HttpGet("checkCustomerAccount")]
        [AllowAnonymous]
        public async Task<JsonResult> checkCustomerAccount(string username,string email, string phonenumber)
        {
            try
            {
                var obj = await _repo.CheckCustomerAccount(username, email, phonenumber);
                if(obj == "accept")
                {
                    return Json(new { obj = obj, status = 1 });

                }
                else { return Json(new { message = obj, status = 0 }); }
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
        [HttpGet("checkEmployeeAccount")]
        [AllowAnonymous]
        public async Task<JsonResult> checkEmployeeAccount(string username, string email, string phonenumber)
        {
            try
            {
                var obj = await _repo.CheckEmployeeAccount(username, email, phonenumber);
                if (obj == "accept")
                {
                    return Json(new { obj = obj, status = 1 });

                }
                else { return Json(new { message = obj, status = 0 }); }
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
        public async Task<JsonResult> UpdateAccount([FromForm] Account acc)
        {
            try
            {            
                var obj = await _repo.UpdateAccount(acc);
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
        public async Task<JsonResult> DeleteAccount([FromForm] string id)
        {
            try
            {
                var obj = await _repo.DeleteAccount(id);
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
        public async Task<JsonResult> SearchAccount([FromBody] postingObject pObject)
        {
            try
            {
                DataList<Account> result = await _repo.SearchAccount(pObject.page, pObject.searchObj);
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
