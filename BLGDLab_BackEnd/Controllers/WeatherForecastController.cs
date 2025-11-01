using BLGDLab.Business.IServices;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace BLGDLab_BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IAuthenticationService _service;// ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IAuthenticationService service)
        {
            // _logger = logger;
            _service = service;
        }

        [HttpGet("GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {


            BaseResponse baseResponse = new BaseResponse();
            HttpStatusCode httpStatusCode = HttpStatusCode.OK;
            try
            {
                var data = await _service.Login(request.userName, request.password);
                if (data == null)
                {
                    baseResponse.Message = "User Not Found!!";
                }
                else
                {
                    baseResponse.Data = data;
                }
            }
            catch (Exception Ex)
            {
                baseResponse.Message = Ex.Message;
                httpStatusCode = HttpStatusCode.InternalServerError;

            }
            return StatusCode((int)httpStatusCode, baseResponse);
        }


    }
}

public class BaseResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public object? Data { get; set; }
}

public class LoginRequest
{
    [Required(ErrorMessage ="User Name is required")]
    public string userName { get; set; }

    [Required(ErrorMessage = "password is required")]
    public string password { get; set; }
    public LoginRequest()
    {
        userName = "";
        password = "";
    }
}
