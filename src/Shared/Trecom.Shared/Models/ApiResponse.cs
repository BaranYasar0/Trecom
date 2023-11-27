using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Trecom.Shared.Models;

public class ApiResponse<T> where T:class
{
    public string Message { get; set; } = String.Empty;
    public T Data { get; set; }
    public List<string> Errors { get; set; } = new();
    public bool IsSuccess { get; set; } = true;
    public int StatusCode { get; set; }
    public string Title { get; set; }
    public DateTime ResponseTime { get; set; }

    public ApiResponse()
    {
        if(!Errors.Any()) IsSuccess = true;
        ResponseTime = DateTime.Now;
    }

    public static ApiResponse<T> Success(T data, string? message = "")
    {
        return new ApiResponse<T>()
        {
            Data = data,
            Message = message,
            StatusCode = 200,
            IsSuccess = true,
            Title = "Success"
        };
    }

    public static ApiResponse<T> Fail(string message, int statusCode = 400)
    {
        return new ApiResponse<T>()
        {
            Message = message,
            StatusCode = statusCode,
            IsSuccess = false,
            Title = "Error"
        };
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

    public bool ValidateSuccess()
    {
        if(Errors is not null && Errors.Any()) return false;
        
        return true;
    }
}


//public class ApiResponse<T> : ApiResponse where T : class
//{
//    public T Data { get; set; }

//    public ApiResponse()
//    {
        
//    }

//    public ApiResponse(T data, string? message = "")
//    {
//        Data = data;
//        Message = message;
//    }

//    public static ApiResponse<T> Success(T data)
//    {
        
//    }


//    public bool ValidateSuccess()
//    {
//        if (Errors is not null && this.Errors.Count > 0)
//        {
//            IsSuccess = false;
//            return false;
//        }

//        return true;
//    }
//}