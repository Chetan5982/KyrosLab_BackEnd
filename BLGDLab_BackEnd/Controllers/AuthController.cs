using BLGDLab.Business.IServices;
using BLGDLab.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BLGDLab_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _service;
        public AuthController(IAuthenticationService service)
        {
            _service = service;
        }

        
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            APIResponse baseResponse = new APIResponse();

            var data = await _service.Login(request.userName, request.password);

            if (data.GetType().GetProperty("userData").GetValue(data) == null)
            {
                baseResponse.Message = "User Not Found!!";
            }
            else
            {
                baseResponse.Data = data;
            }
            return StatusCode((int)HttpStatusCode.OK, baseResponse);
        }
    }
}
