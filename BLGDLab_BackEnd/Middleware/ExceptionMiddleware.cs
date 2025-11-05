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

        public async void Invoke(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            catch (Exception Ex)
            {
                HandleExecption(context,Ex);
            }
        }

        private void HandleExecption(HttpContext context,Exception exception)
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
                Success = false,
                Message = exception.Message,
                StatusCode = context.Response.StatusCode,
            });

            context.Response.WriteAsync(result);
        }


    }
}
