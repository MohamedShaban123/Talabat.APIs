using System.Net;
using System.Text.Json;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> logger,IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
           try
            {
                await next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex,ex.Message); // Developement
                // Log Exception in (Database | Files) // Production


              // header of response 
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode= (int)HttpStatusCode.InternalServerError;

                // body of response
                var response = env.IsDevelopment() ? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                                                   : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

                // Convert object to json file because writeAsync take json file
                // if we need to convert json to object we use Deserializer method
                // if we need to convert object to object we use serializer method
                var json = JsonSerializer.Serialize(response);
              await  httpContext.Response.WriteAsync(json);
            }
        }
    }
}
