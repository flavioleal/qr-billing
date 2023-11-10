using QR.Billings.Business.BusinessObjects;
using System.Text.Json;
using System.Text;
using Serilog;

namespace QR.Billings.Api.Middleware
{
    /// <summary>
    /// Middleware for handling errors in the application.
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next"></param>
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invokes the middleware to handle errors in the HTTP request pipeline.
        /// </summary>
        /// <param name="context">The HTTP context for the current request.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessException businessException)
            {
                context.Response.Clear();
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(businessException.Message);
                await context.Response.WriteAsync(result, Encoding.UTF8);
                return;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro não tratado");

                context.Response.StatusCode = 500;
                throw;
            }
        }
    }
}
