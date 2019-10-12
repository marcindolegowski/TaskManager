using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerValidationService.Services.Comunication
{
    public class BaseResponse
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }

        public BaseResponse()
        {
            Success = true;
            Message = string.Empty;
        }

        public BaseResponse(string message)
        {
            Success = false;
            Message = message;
        }
    }
}
