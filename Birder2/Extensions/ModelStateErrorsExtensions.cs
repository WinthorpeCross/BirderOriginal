using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Birder2.Extensions
{
    public static class ModelStateErrorsExtensions
    {
        public static string GetModelStateErrorMessages(ModelStateDictionary modelState)
        {
            string validationErrors = string.Join("; ",
                    modelState.Values.Where(e => e.Errors.Count > 0)
                        .SelectMany(e => e.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToArray());

            return validationErrors;
        }
    }
}