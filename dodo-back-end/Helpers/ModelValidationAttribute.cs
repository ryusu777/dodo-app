using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DodoApp.Helpers
{
    public class ModelValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                List<string> errors = new List<string>();
                foreach (var modelStateKey in context.ModelState.Keys)
                {
                    var modelStateVal = context.ModelState[modelStateKey];
                    foreach (var error in modelStateVal.Errors)
                    {
                        var key = modelStateKey;
                        errors.Add(error.ErrorMessage);
                    }
                }
                context.Result = new BadRequestObjectResult(new 
                    { errors = errors }
                );
            }
        }
    }
}