using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Shared.Utils;

public static class ApiResponseHelpers
{

    public static ApiResponse InternalServerError(string message = "Request Failed") => new ApiResponse
    {
        Success = false,
        Title = "Internal Server Error",
        Message = message,
        StatusCode = 500
    };
    public static ApiResponse BadRequest(string message = "Bad Request") => new ApiResponse
    {
        Success = false,
        Title = "Bad Request",
        Message = message,
        StatusCode = 400
    };
    public static ApiResponse Unauthorized(string message = "Unauthorized") => new ApiResponse
    {
        Success = false,
        Title = "Unauthorized",
        Message = message,
        StatusCode = 401
    };
    public static ApiResponse NotFound(string message = "Resource Not Found") => new ApiResponse
    {
        Success = false,
        Title = "Not Found",
        Message = message,
        StatusCode = 404
    };
    public static ApiResponse NoContext(string message = "Succesfully") => new ApiResponse
    {
        Success = false,
        Title = "No Context",
        Message = message,
        StatusCode = 204
    };
    public static ApiResponse Created(string message = "Resource Created Successfully") => new ApiResponse
    {
        Success = true,
        Title = "Created",
        Message = message,
        StatusCode = 201
    };
    public static ApiResponse Ok(string message = "Succesfully") => new ApiResponse
    {
        Success = true,
        Title = "OK",
        Message = message,
        StatusCode = 200
    };

    public static ApiResponse<T> Ok<T>(T data, string message = "Succesfully") => new ApiResponse<T>
    {
        Success = true,
        Title = "OK",
        Message = message,
        StatusCode = 200,
        Data = data
    };
    public static ApiResponse<T> Created<T>(T data, string message = "Resource Created Successfully") => new ApiResponse<T>
    {
        Success = true,
        Title = "Created",
        Message = message,
        StatusCode = 201,
        Data = data
    };
    public static ApiResponse<T> BadRequest<T>(string message = "Bad Request") => new ApiResponse<T>
    {
        Success = false,
        Title = "Bad Request",
        Message = message,
        StatusCode = 400,
        Data = default
    };
    public static ApiResponse<T> NotFound<T>(string message = "Resource Not Found") => new ApiResponse<T>
    {
        Success = false,
        Title = "Not Found",
        Message = message,
        StatusCode = 404,
        Data = default
    };

    public static ApiResponse<T> Unauthorized<T>(string message = "Unauthorized") => new ApiResponse<T>
    {
        Success = false,
        Title = "Unauthorized",
        Message = message,
        StatusCode = 401,
        Data = default
    };

    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }

    }
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public T? Data { get; set; }
        
    }
}
