using FinTech_ApiPanel.Application.Common;
using System.Net;

namespace FinTech_ApiPanel.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");

                context.Response.ContentType = "application/json";

                ApiResponse response;
                int statusCode;

                switch (ex)
                {
                    case UnauthorizedAccessException:
                        statusCode = (int)HttpStatusCode.Unauthorized;
                        response = ApiResponse.UnauthorizedResponse("Unauthorized access.");
                        break;

                    case ArgumentException argEx:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        response = ApiResponse.BadRequestResponse(argEx.Message);
                        break;

                    default:
                        statusCode = (int)HttpStatusCode.InternalServerError;
                        var message = _env.IsDevelopment()
                            ? $"{ex.Message} {ex.StackTrace}"
                            : "An unexpected error occurred.";
                        response = ApiResponse.InternalServerErrorResponse(message);
                        break;
                }

                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
