using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.api
{
    public class ErrorResponse
    {
        public string ErrorMessage { get; set; }

        public ErrorResponse(string message)
        {
            ErrorMessage = message;
        }
    }
}
