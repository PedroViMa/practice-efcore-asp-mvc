using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using StoreWebApp_Model.Models;

namespace StoreWebApp_API.Filters.ActionFilters.SaleValidations
{
    public class Sale_ValidateUpdateSaleFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var id = context.ActionArguments["id"] as int?;
            var sale = context.ActionArguments["sale"] as Sale;

            if (id.HasValue && sale != null && id != sale.Id)
            {
                context.ModelState.AddModelError("SaleId", "SaleId is not the same as id.");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }
    }
}
