using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StoreWebApp_DAL.DAO.AdoRep;
using StoreWebApp_DAL.DAO.Interfaces;
using StoreWebApp_DAL.Data;

namespace StoreWebApp_API.Filters.ExceptionFilters
{
    public class Sale_HandleUpdateExceptionsFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IRepSale repSale;
        public Sale_HandleUpdateExceptionsFilterAttribute(StoreDbContext db)
        {
            repSale = new AdoSaleRep();
        }

        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            var strSaleId = context.RouteData.Values["id"] as string;
            if (int.TryParse(strSaleId, out int saleId))
            {
                if (!repSale.SaleExists(saleId))
                {
                    context.ModelState.AddModelError("Sale", "Sale does not exist anymore.");
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
