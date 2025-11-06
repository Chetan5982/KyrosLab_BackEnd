using BLGDLab.Business.IServices;
using BLGDLab.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BLGDLab_BackEnd.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogsAsync(string jsonString)
        {
            APIResponse api = new APIResponse();
            HttpStatusCode httpStatusCode = HttpStatusCode.OK;
            try
            {
                api.Data = await _blogService.GetAllBlogsAsync(jsonString);
            }
            catch (Exception ex)
            {
                httpStatusCode = HttpStatusCode.InternalServerError;
                api.Message = ex.Message;
            }
            return StatusCode((int)httpStatusCode, api);
        }
    }
}
