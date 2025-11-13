using BLGDLab.Business.Models;
using System.Net;
using System.Text.Json;

namespace BLGDLab_BackEnd.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _request;
        public ExceptionMiddleware(RequestDelegate request)
        {
            _request = request;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            catch (Exception Ex)
            {
                await HandleExecption(context,Ex);
            }
        }

        private async Task HandleExecption(HttpContext context,Exception exception)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = exception switch
            {
                ArgumentNullException => (int)HttpStatusCode.BadRequest,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var result = JsonSerializer.Serialize(new APIResponse
            {
                Status = false,
                Message = exception.Message,
                StatusCode = context.Response.StatusCode,
            });

            await context.Response.WriteAsync(result);
        }


    }
}
