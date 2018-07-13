using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Birder2.Extensions
{
    public static class ModelStateErrorsExtensions
    {
        public static string GetModelStateErrorMessages(ModelStateDictionary ModelState)
        {
            string validationErrors = string.Join("; ",
                    ModelState.Values.Where(E => E.Errors.Count > 0)
                    .SelectMany(E => E.Errors)
                    .Select(E => E.ErrorMessage)
                    .ToArray());

            return validationErrors;
        }
    }
}