using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StoreWebApp_Model.Models;

namespace StoreWebApp_API.Filters.ActionFilters.ProductValidations
{
    public class Product_ValidateCreateProductFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var product = context.ActionArguments["product"] as Product;

            if (product != null)
            {
                context.ModelState.AddModelError("Product", "Product object is null.");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }
    }
}