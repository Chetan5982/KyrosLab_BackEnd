using BLGDLab.Business.IServices;
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
            BaseResponse baseResponse = new BaseResponse();

            var data = await _service.Login(request.userName, request.password);

            if (data == null)
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
