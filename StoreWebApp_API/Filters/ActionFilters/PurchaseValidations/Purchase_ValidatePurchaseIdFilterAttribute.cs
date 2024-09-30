using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StoreWebApp_DAL.DAO.AdoRep;
using StoreWebApp_DAL.DAO.EFCRep;
using StoreWebApp_DAL.DAO.Interfaces;
using StoreWebApp_DAL.Data;

namespace StoreWebApp_API.Filters.ActionFilters.PurchaseValidations
{
    public class Purchase_ValidatePurchaseIdFilterAttribute : ActionFilterAttribute
    {
        private readonly IRepPurchase repPurchase;

        public Purchase_ValidatePurchaseIdFilterAttribute(StoreDbContext db)
        {
            repPurchase = new AdoPurchaseRep();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var purchaseId = context.ActionArguments["id"] as int?;
            if (purchaseId.HasValue)
            {
                if (purchaseId.Value <= 0)
                {
                    context.ModelState.AddModelError("PurchaseId", "PurchaseId is invalid.");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
                else
                {
                    if (!repPurchase.PurchaseExists(purchaseId))
                    {
                        context.ModelState.AddModelError("PurchaseId", "Purchase doesn't exist.");
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
