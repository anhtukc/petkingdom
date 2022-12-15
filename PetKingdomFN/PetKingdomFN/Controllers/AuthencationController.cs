using Microsoft.AspNetCore.Mvc;
using PetKingdomFN.Interfaces;
using System.Net;

namespace PetKingdomFN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthencationController : Controller
    {

        private readonly IJwtUtils _repository;
        public AuthencationController(IJwtUtils repository)
        {
            _repository = repository;
        }
        [HttpPost("Authenticate")]
        public async Task<ActionResult> Authenticate(string username, string password)
        {
            try
            {           

                string result = await _repository.GenerateJwtToken(username, password);
                if (result == "Invalid account")
                {
                    return Json(new { message = "fail", details = result });
                }

                return Json(new { token = result, message = "success" });
            }
            catch(WebException ex)
            {
                HttpStatusCode statusCode = ((HttpWebResponse)ex.Response).StatusCode;
                return Json(new {error_code = statusCode, message = "fail", details = ex.Message});
            }
        }
    }
}
