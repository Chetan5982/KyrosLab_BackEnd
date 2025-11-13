using BLGDLab.Business.IServices;
using BLGDLab.Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BLGDLab_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiamondSearchController : ControllerBase
    {
        private readonly IDiamondSearchService _diamondSearchService;
        private  APIResponse _response;
        public DiamondSearchController(IDiamondSearchService diamondSearchService, IConfiguration configuration)
        {
            _diamondSearchService=diamondSearchService;
            _response=new APIResponse();
        }

        [HttpGet("Filters")]
        public async Task<IActionResult> SearchDiamondFilters([FromQuery] bool isForDataSet,bool isIncludeOnlyInstockCriteria) 
        { 
            HttpStatusCode httpStatusCode = HttpStatusCode.OK;
            var data =await _diamondSearchService.GetDiamondFilter(isForDataSet,1,isIncludeOnlyInstockCriteria);
            if (data == null)
            {
                httpStatusCode = HttpStatusCode.NotFound;
                _response.StatusCode = (int)HttpStatusCode.NotFound;
                _response.Message = "No Filter Found";
            }
            else
            {
                _response.Data = data;
                _response.StatusCode = (int)HttpStatusCode.OK;
            }

            return StatusCode((int)httpStatusCode, _response);

        }
    }
}
