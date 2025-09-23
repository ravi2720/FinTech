using FinTech_ApiPanel.Domain.Interfaces.IServiceMasters;
using FinTech_ApiPanel.Domain.Interfaces.IUserServices;
using FinTech_ApiPanel.Domain.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinTech_ApiPanel.API.Middleware
{
    public class ServiceValidate : Attribute, IAsyncAuthorizationFilter
    {
        private readonly ServiceType serviceType;

        public ServiceValidate(ServiceType serviceName)
        {
            this.serviceType = serviceName;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var httpContext = context.HttpContext;

            var serviceMasterChecker = context.HttpContext.RequestServices.GetService<IServiceMasterRepository>();
            var userServiceChecker = context.HttpContext.RequestServices.GetService<IUserServiceRepository>();

            if (serviceMasterChecker == null || userServiceChecker == null)
            {
                var errorResponse = new
                {
                    statuscode = "ERR",
                    status = "Something went wrong",
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

            var userId = httpContext.Items["UserId"]?.ToString();

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User ID not found in the context.");
            }

            var serviceMasterList = await serviceMasterChecker.GetAllAsync();

            var golobalService = serviceMasterList.FirstOrDefault(x => x.Type == (byte)serviceType);

            if (golobalService == null || !golobalService.IsActive)
            {
                var errorResponse = new
                {
                    statuscode = "ERR",
                    status = "Service not available.",
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

            var userServices = await userServiceChecker.GetByUserIdAndServiceIdAsync(long.Parse(userId), golobalService.Id);

            if (userServices == null || !userServices.IsActive)
            {
                var errorResponse = new
                {
                    statuscode = "ERR",
                    status = "Service not available.",
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
        }
    }
}
