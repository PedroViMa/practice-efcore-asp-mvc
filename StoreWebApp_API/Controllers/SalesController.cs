using Microsoft.AspNetCore.Mvc;
using StoreWebApp_API.Filters.ActionFilters.PurchaseValidations;
using StoreWebApp_API.Filters.ActionFilters.SaleValidations;
using StoreWebApp_API.Filters.AuthFilters;
using StoreWebApp_API.Filters.ExceptionFilters;
using StoreWebApp_DAL.DAO.AdoRep;
using StoreWebApp_DAL.DAO.Interfaces;
using StoreWebApp_DAL.Data;
using StoreWebApp_Model.Models;

namespace StoreWebApp_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [JwtTokenAuthFilter]
    public class SalesController : ControllerBase
    {
        private readonly IRepSale repSale;

        public SalesController(StoreDbContext db)
        {
            repSale = new AdoSaleRep();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(repSale.GetSales());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(Sale_ValidateSaleIdFilterAttribute))]
        public IActionResult Details(int id)
        {
            return Ok(repSale.GetSaleById(id));
        }

        [HttpPost]
        [Sale_ValidateCreateSaleFilter]
        public IActionResult Create([FromBody] Sale sale)
        {
            repSale.CreateSale(sale);

            return CreatedAtAction(nameof(Details),
                new { id = sale.Id },
                sale);
        }

        [HttpPut("{id}")]
        [TypeFilter(typeof(Sale_ValidateSaleIdFilterAttribute))]
        [Sale_ValidateUpdateSaleFilter]
        [TypeFilter(typeof(Sale_HandleUpdateExceptionsFilterAttribute))]
        public IActionResult Edit(int id, [FromBody] Sale sale)
        {
            repSale.UpdateSale(sale);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [TypeFilter(typeof(Sale_ValidateSaleIdFilterAttribute))]
        public IActionResult Delete(int id)
        {
            repSale.DeleteSale(id);

            return Ok(HttpContext.Items["purchase"]);
        }
    }
}
