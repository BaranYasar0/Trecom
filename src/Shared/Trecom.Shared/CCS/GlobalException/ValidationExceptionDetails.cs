using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Trecom.Shared.CCS.GlobalException
{
    public class ValidationExceptionDetails
    {
        public object Errors { get; set; }
        
        public int StatusCode { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string RequestName { get; set; }
        public DateTime ThrownDate { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
