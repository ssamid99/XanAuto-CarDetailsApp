﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace XanAuto.Application.AppCode.Extensions
{
    public static partial class Extension
    {
        public static IEnumerable<ValidationError> GetErrors(this ModelStateDictionary modelState)
        {
            var errors = (from key in modelState.Keys
                          where modelState[key] != null
                          select new ValidationError(key, modelState[key].Errors[0].ErrorMessage)
                          ).ToList();

            return errors;
        }
    }

    public class ValidationError
    {
        public string FieldName { get; set; }
        public string Message { get; set; }

        public ValidationError(string fieldName, string message)
        {
            this.FieldName = fieldName;
            this.Message = message;
        }
    }
}
