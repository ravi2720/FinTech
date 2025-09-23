using FinTech_ApiPanel.Domain.Interfaces.IAuth;
using FinTech_ApiPanel.Domain.Interfaces.IUserMasters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace FinTech_ApiPanel.API.Middleware
{
    public class IPayValidate : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var httpContext = context.HttpContext;
            var headers = httpContext.Request.Headers;

            try
            {
                if (!headers.TryGetValue("Client-Id", out var clientId))
                {
                    var errorResponse = new
                    {
                        statuscode = "ERR",
                        status = "Client-Id is missing",
                        timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    };

                    context.Result = new ContentResult
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Content = System.Text.Json.JsonSerializer.Serialize(errorResponse),
                        ContentType = "application/json"
                    };

                    return;
                }

                if (!headers.TryGetValue("Client-Secret", out var clientSecret))
                {
                    var errorResponse = new
                    {
                        statuscode = "ERR",
                        status = "Client-Secret is missing",
                        timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    };

                    context.Result = new ContentResult
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Content = System.Text.Json.JsonSerializer.Serialize(errorResponse),
                        ContentType = "application/json"
                    };

                    return;
                }

                if (!headers.TryGetValue("Endpoint-Ip", out var endpointIp) || !IPAddress.TryParse(endpointIp, out var parsedIp))
                {
                    var errorResponse = new
                    {
                        statuscode = "ERR",
                        status = "Endpoint-Ip is missing or invalid",
                        timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    };

                    context.Result = new ContentResult
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Content = System.Text.Json.JsonSerializer.Serialize(errorResponse),
                        ContentType = "application/json"
                    };

                    return;
                }

                var clientRepo = httpContext.RequestServices.GetService<IClientRepository>();
                var client = await clientRepo.ValidateClient(clientId, clientSecret);

                if (client == null)
                {
                    var errorResponse = new
                    {
                        statuscode = "ERR",
                        status = "Invalid client credentials",
                        timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    };

                    context.Result = new ContentResult
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Content = System.Text.Json.JsonSerializer.Serialize(errorResponse),
                        ContentType = "application/json"
                    };

                    return;
                }

                var userRepo = httpContext.RequestServices.GetService<IUserRepository>();
                var user = await userRepo.GetByIdAsync(client.UserId);

                if (user == null)
                {
                    var errorResponse = new
                    {
                        statuscode = "ERR",
                        status = "User dont exist",
                        timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    };

                    context.Result = new ContentResult
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Content = System.Text.Json.JsonSerializer.Serialize(errorResponse),
                        ContentType = "application/json"
                    };

                    return;
                }

                if (!user.IsActive)
                {
                    var errorResponse = new
                    {
                        statuscode = "ERR",
                        status = "Access blocked for the user please contact admin.",
                        timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    };

                    context.Result = new ContentResult
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Content = System.Text.Json.JsonSerializer.Serialize(errorResponse),
                        ContentType = "application/json"
                    };

                    return;
                }

                httpContext.Items["Client-Id"] = client.ClientId.ToString();
                httpContext.Items["Client-Secret"] = client.ClientSecret.ToString();
                httpContext.Items["UserId"] = client.UserId.ToString();
                httpContext.Items["Encryption-Key"] = client.EncryptionKey.ToString();
                httpContext.Items["Endpoint-Ip"] = parsedIp.ToString();

                if (headers.TryGetValue("Outlet-Id", out var outletid))
                    httpContext.Items["Outlet-Id"] = outletid.ToString();
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    statuscode = "ERR",
                    status = $"IPayValidate filter error: {ex.Message}",
                    timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                };

                context.Result = new ContentResult
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Content = System.Text.Json.JsonSerializer.Serialize(errorResponse),
                    ContentType = "application/json"
                };

                return;
            }
        }
    }
}
