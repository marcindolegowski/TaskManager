using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Validators
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            Success = true;
        }
        public ValidationResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
            Success = false;
        }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
    }
}
