using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Http;

namespace Trecom.Shared.CCS.GlobalException
{
    public class BusinessException : Exception
    {
        public BusinessException()
        {
        }

        public BusinessException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public BusinessException(string? message) : base(message)
        {
        }
    }

    public class BusinessExceptionDetails
    {
        public int Status { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string RequestName { get; set; }
        public int StatusCode { get; set; }
        public DateTime ThrownDate { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
