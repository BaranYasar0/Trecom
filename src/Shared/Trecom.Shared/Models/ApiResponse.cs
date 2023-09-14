using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Trecom.Api.Services.Catalog.Extensions;

namespace Trecom.Shared.Models;

public class ApiResponse
{
    public string Message { get; set; } = String.Empty;
    public List<string> Errors { get; set; }
    public bool IsSuccess { get; set; } = true;
    public int StatusCode { get; set; }
    public string Title { get; set; }
    public DateTime ResponseTime { get; set; }

    //public ApiResponse()
    //{
    //    SetSuccess();
    //}

    

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}


public class ApiResponse<T> : ApiResponse where T : class
{
    public T Data { get; set; }

    public ApiResponse()
    {
        
    }

    public ApiResponse(T data, string? message = "")
    {
        Data = data;
        Message = message;
    }

    public bool ValidateSuccess()
    {
        if (Errors is not null && this.Errors.Count > 0)
        {
            IsSuccess = false;
            return false;
        }

        return true;
    }
}