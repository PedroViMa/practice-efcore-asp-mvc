using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StoreWebApp_Model.Models;

namespace StoreWebApp_API.Filters.ActionFilters.SaleValidations
{
    public class Sale_ValidateCreateSaleFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var sale = context.ActionArguments["sale"] as Sale;

            if (sale != null)
            {
                context.ModelState.AddModelError("Sale", "Sale object is null.");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }
    }
}