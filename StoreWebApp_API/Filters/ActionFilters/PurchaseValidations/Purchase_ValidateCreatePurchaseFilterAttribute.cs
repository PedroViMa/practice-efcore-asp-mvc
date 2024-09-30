using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StoreWebApp_Model.Models;

namespace StoreWebApp_API.Filters.ActionFilters.PurchaseValidations
{
    public class Purchase_ValidateCreatePurchaseFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var purchase = context.ActionArguments["purchase"] as Purchase;

            if (purchase != null)
            {
                context.ModelState.AddModelError("Purchase", "Purchase object is null.");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }
    }
}