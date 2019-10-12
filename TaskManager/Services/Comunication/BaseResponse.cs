﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Services.Comunication
{
    public class BaseResponse<T>
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public T Resource { get; private set; }

        public int NumberOfStateChanges { get; set; }

        public BaseResponse(T resource)
        {
            Success = true;
            Message = string.Empty;
            Resource = resource;
        }

        public BaseResponse(string message)
        {
            Success = false;
            Message = message;
            Resource = default;
        }
    }
}
