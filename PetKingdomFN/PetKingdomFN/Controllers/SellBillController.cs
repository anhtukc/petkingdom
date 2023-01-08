using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.SignalR;
using PetKingdomFN.BusEntities;
using PetKingdomFN.Interfaces;
using PetKingdomFN.Models;
using SocketIOClient;
using System.Net.Sockets;

namespace PetKingdomFN.Controllers
{
    [Route("api/[controller]")]
    public class SellBillsController : Controller
    {

        private readonly ISellBillRepository _repo;
        private readonly IHubContext<SellBillHub> _hub;

        public SellBillsController(ISellBillRepository repo, IHubContext<SellBillHub> hub )
        {
            this._repo = repo;
            _hub = hub;
        }

        [HttpPost("getPage")]
        [Authorize]
        public async Task<IActionResult> GetPageByStatus([FromBody] postingObject pObject)
        {
            try
            {
                DataList<SellBill> result = await _repo.GetPageList(pObject.page, pObject.searchObj);
                await _hub.Clients.All.SendAsync("pageData", new
                {
                    list = result.list,
                    numberOfRecords = result.numberOfRecords,
                    status = 1
                });
                return Ok(new { Message = "Request Completed" });
            }
            catch (Exception ex)
            {
                await _hub.Clients.All.SendAsync("pageData", new
                {
                    status = 0,
                    details = ex.Message
                });
                return BadRequest();

            }
        }
        [HttpGet("getStatus")]
        [Authorize]
        public async Task<IActionResult> GetAllStatus()
        {
            try
            {
                List<CountStatus> result = await _repo.GetAllStatus();
                await _hub.Clients.All.SendAsync("statusList", new
                {
                    statusList = result,
                    status = 1
                });
                return Ok(new { Message = "Request Completed" });
            }
            catch (Exception ex)
            {
                await _hub.Clients.All.SendAsync("Error", ex.Message);
                return BadRequest();
            }
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<JsonResult> AddSellBill([FromForm] SellBill sellBill)
        {
            try
            {

                SellBill obj = await _repo.AddSellBill(sellBill);
                await _hub.Clients.All.SendAsync("addSellBill", new
                {
                    obj = obj,
                    status = 1
                });
                return Json(new { obj = obj, status = 1 });
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, details = ex.Message });
            }

        }
        [HttpGet("getById")]
        [Authorize]
        public async Task<JsonResult> GetSellBillById([FromQuery] string id)
        {
            try
            {
                var obj = await _repo.GetSellBillById(id);
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
        public async Task<JsonResult> UpdateSellBill([FromForm] SellBill sellBill)
        {
            try
            {
                
                var obj = await _repo.UpdateSellBill(sellBill);
                await _hub.Clients.All.SendAsync("updateSellBill", new
                {
                    obj = sellBill,
                    status = sellBill.Status
                }) ;
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
        public async Task<JsonResult> DeleteSellBill([FromForm] string id)
        {
            try
            {
                var obj = await _repo.DeleteSellBill(id);
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

       
    }
}
