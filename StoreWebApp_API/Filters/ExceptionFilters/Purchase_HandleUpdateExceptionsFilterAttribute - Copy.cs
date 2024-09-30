using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StoreWebApp_DAL.DAO.EFCRep;
using StoreWebApp_DAL.DAO.Interfaces;
using StoreWebApp_DAL.Data;

namespace StoreWebApp_API.Filters.ExceptionFilters
{
    public class Purchase_HandleUpdateExceptionsFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IRepPurchase repPurchase;
        public Purchase_HandleUpdateExceptionsFilterAttribute(StoreDbContext db)
        {
            repPurchase = new EFCPurchaseRep(db);
        }

        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            var strPurchaseId = context.RouteData.Values["id"] as string;
            if (int.TryParse(strPurchaseId, out int purchaseId))
            {
                if (!repPurchase.PurchaseExists(purchaseId))
                {
                    context.ModelState.AddModelError("Purchase", "Purchase does not exist anymore.");
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
