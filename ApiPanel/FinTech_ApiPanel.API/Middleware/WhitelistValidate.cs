using FinTech_ApiPanel.Domain.Interfaces.IWhitelists;
using FinTech_ApiPanel.Domain.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinTech_ApiPanel.API.Middleware
{
    public class WhitelistValidate : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var httpContext = context.HttpContext;

            var remoteIpAddress = httpContext.Connection.RemoteIpAddress?.ToString();

            if (string.IsNullOrEmpty(remoteIpAddress))
            {
                var errorResponse = new
                {
                    statuscode = "ERR",
                    status = "Access denied for the IP",
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

            var whitelistRepo = httpContext.RequestServices.GetService<IWhitelistRepository>();

            if (whitelistRepo == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                return;
            }

            var userId = httpContext.Items["UserId"]?.ToString();

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User ID not found in the context.");
            }

            var whitelist = await whitelistRepo.GetByUserIdAsync(long.Parse(userId));

            if (!whitelist.Any())
            {
                var errorResponse = new
                {
                    statuscode = "ERR",
                    status = $"Access denied for the IP",
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


            var isIpWhitelist = whitelist.Any(x => x.Value == remoteIpAddress && x.EntryType == (byte)WhitelistEntryType.IP);

            if (!isIpWhitelist)
            {
                var errorResponse = new
                {
                    statuscode = "ERR",
                    status = $"Invalid IP address: {remoteIpAddress}",
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

            httpContext.Items["Remote-Ip"] = remoteIpAddress;
        }
    }
}
