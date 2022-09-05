using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public List<string> ErrorList { get; set; } = new List<string>();
        public ValidationException(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                ErrorList.Add(error.ErrorMessage);
            }
        }
    }
}
