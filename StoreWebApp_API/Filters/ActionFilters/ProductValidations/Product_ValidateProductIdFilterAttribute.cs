using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StoreWebApp_DAL.DAO.AdoRep;
using StoreWebApp_DAL.DAO.EFCRep;
using StoreWebApp_DAL.DAO.Interfaces;
using StoreWebApp_DAL.Data;

namespace StoreWebApp_API.Filters.ActionFilters.ProductValidations
{
    public class Product_ValidateProductIdFilterAttribute : ActionFilterAttribute
    {
        private readonly IRepProduct repProduct;

        public Product_ValidateProductIdFilterAttribute(StoreDbContext db)
        {
            repProduct = new AdoProductRep();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var productId = context.ActionArguments["id"] as int?;
            if (productId.HasValue)
            {
                if (productId.Value <= 0)
                {
                    context.ModelState.AddModelError("ProductId", "ProductId is invalid.");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
                else
                {
                    if (!repProduct.ProductExists(productId))
                    {
                        context.ModelState.AddModelError("ProductId", "Product doesn't exist.");
                        var problemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Status = StatusCodes.Status404NotFound
                        };
                        context.Result = new NotFoundObjectResult(problemDetails);
                    }
                }
            }
        }
    }
}
