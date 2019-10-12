using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Services.Comunication.TaskValidationService
{
    public class ValidationResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
