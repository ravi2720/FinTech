using System.Net;

namespace FinTech_ApiPanel.Application.Common
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }

        // 200 OK - no data
        public static ApiResponse SuccessResponse(string message = "Success")
        {
            return new ApiResponse
            {
                Success = true,
                Message = message,
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        // 201 Created - no data
        public static ApiResponse CreatedResponse(string message = "Resource created successfully")
        {
            return new ApiResponse
            {
                Success = true,
                Message = message,
                StatusCode = (int)HttpStatusCode.Created
            };
        }

        // 204 No Content
        public static ApiResponse NoContentResponse(string message = "No content")
        {
            return new ApiResponse
            {
                Success = true,
                Message = message,
                StatusCode = (int)HttpStatusCode.NoContent
            };
        }

        // Errors
        public static ApiResponse BadRequestResponse(string message = "Bad Request")
            => Error(message, HttpStatusCode.BadRequest);

        public static ApiResponse UnauthorizedResponse(string message = "Unauthorized")
            => Error(message, HttpStatusCode.Unauthorized);

        public static ApiResponse ForbiddenResponse(string message = "Forbidden")
            => Error(message, HttpStatusCode.Forbidden);

        public static ApiResponse NotFoundResponse(string message = "Not Found")
            => Error(message, HttpStatusCode.NotFound);

        public static ApiResponse ConflictResponse(string message = "Conflict")
            => Error(message, HttpStatusCode.Conflict);

        public static ApiResponse ValidationErrorResponse(string message = "Validation Error")
            => Error(message, HttpStatusCode.UnprocessableEntity);

        public static ApiResponse InternalServerErrorResponse(string message = "Internal Server Error")
            => Error(message, HttpStatusCode.InternalServerError);

        private static ApiResponse Error(string message, HttpStatusCode statusCode)
        {
            return new ApiResponse
            {
                Success = false,
                Message = message,
                StatusCode = (int)statusCode
            };
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T? Data { get; set; }

        // 200 OK with data
        public static ApiResponse<T> SuccessResponse(T? data = default, string message = "Success")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                StatusCode = (int)HttpStatusCode.OK,
                Data = data
            };
        }

        // 201 Created with data
        public static ApiResponse<T> CreatedResponse(T? data = default, string message = "Resource created successfully")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                StatusCode = (int)HttpStatusCode.Created,
                Data = data
            };
        }

        // 204 No Content - no data, but typed
        public static ApiResponse<T> NoContentResponse(string message = "No content")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                StatusCode = (int)HttpStatusCode.NoContent,
                Data = default
            };
        }

        // Errors
        public static ApiResponse<T> BadRequestResponse(string message = "Bad Request")
            => Error(message, HttpStatusCode.BadRequest);

        public static ApiResponse<T> UnauthorizedResponse(string message = "Unauthorized")
            => Error(message, HttpStatusCode.Unauthorized);

        public static ApiResponse<T> ForbiddenResponse(string message = "Forbidden")
            => Error(message, HttpStatusCode.Forbidden);

        public static ApiResponse<T> NotFoundResponse(string message = "Not Found")
            => Error(message, HttpStatusCode.NotFound);

        public static ApiResponse<T> ConflictResponse(string message = "Conflict")
            => Error(message, HttpStatusCode.Conflict);

        public static ApiResponse<T> ValidationErrorResponse(string message = "Validation Error")
            => Error(message, HttpStatusCode.UnprocessableEntity);

        public static ApiResponse<T> InternalServerErrorResponse(string message = "Internal Server Error")
            => Error(message, HttpStatusCode.InternalServerError);

        private static ApiResponse<T> Error(string message, HttpStatusCode statusCode)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                StatusCode = (int)statusCode,
                Data = default
            };
        }
    }
}
