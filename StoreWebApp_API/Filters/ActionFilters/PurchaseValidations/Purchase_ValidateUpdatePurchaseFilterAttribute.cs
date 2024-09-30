using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using StoreWebApp_Model.Models;

namespace StoreWebApp_API.Filters.ActionFilters.PurchaseValidations
{
    public class Purchase_ValidateUpdatePurchaseFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var id = context.ActionArguments["id"] as int?;
            var purchase = context.ActionArguments["purchase"] as Purchase;

            if (id.HasValue && purchase != null && id != purchase.Id)
            {
                context.ModelState.AddModelError("PurchaseId", "PurchaseId is not the same as id.");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }
    }
}
