using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StoreWebApp_DAL.DAO.EFCRep;
using StoreWebApp_DAL.DAO.Interfaces;
using StoreWebApp_DAL.Data;

namespace StoreWebApp_API.Filters.ExceptionFilters
{
    public class Product_HandleUpdateExceptionsFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IRepProduct repProduct;
        public Product_HandleUpdateExceptionsFilterAttribute(StoreDbContext db)
        {
            repProduct = new EFCProductRep(db);
        }

        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            var strProductId = context.RouteData.Values["id"] as string;
            if (int.TryParse(strProductId, out int productId))
            {
                if (!repProduct.ProductExists(productId))
                {
                    context.ModelState.AddModelError("Product", "Product does not exist anymore.");
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
