using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StoreWebApp_DAL.DAO.AdoRep;
using StoreWebApp_DAL.DAO.Interfaces;
using StoreWebApp_DAL.Data;

namespace StoreWebApp_API.Filters.ActionFilters.SaleValidations
{
    public class Sale_ValidateSaleIdFilterAttribute : ActionFilterAttribute
    {
        private readonly IRepSale repSale;

        public Sale_ValidateSaleIdFilterAttribute(StoreDbContext db)
        {
            repSale = new AdoSaleRep();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var saleId = context.ActionArguments["id"] as int?;
            if (saleId.HasValue)
            {
                if (saleId.Value <= 0)
                {
                    context.ModelState.AddModelError("SaleId", "SaleId is invalid.");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
                else
                {
                    if (!repSale.SaleExists(saleId))
                    {
                        context.ModelState.AddModelError("SaleId", "Sale doesn't exist.");
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
