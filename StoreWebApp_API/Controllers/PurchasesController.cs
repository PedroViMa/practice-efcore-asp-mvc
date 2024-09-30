using Microsoft.AspNetCore.Mvc;
using StoreWebApp_API.Filters.ActionFilters.PurchaseValidations;
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
    public class PurchasesController : ControllerBase
    {
        private readonly IRepPurchase repPurchase;

        public PurchasesController(StoreDbContext db)
        {
            repPurchase = new AdoPurchaseRep();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(repPurchase.GetPurchases());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(Purchase_ValidatePurchaseIdFilterAttribute))]
        public IActionResult Details(int id)
        {
            return Ok(repPurchase.GetPurchaseById(id));
        }

        [HttpPost]
        [Purchase_ValidateCreatePurchaseFilter]
        public IActionResult Create([FromBody] Purchase purchase)
        {
            repPurchase.CreatePurchase(purchase);

            return CreatedAtAction(nameof(Details),
                new { id = purchase.Id },
                purchase);
        }

        [HttpPut("{id}")]
        [TypeFilter(typeof(Purchase_ValidatePurchaseIdFilterAttribute))]
        [Purchase_ValidateUpdatePurchaseFilter]
        [TypeFilter(typeof(Purchase_HandleUpdateExceptionsFilterAttribute))]
        public IActionResult Edit(int id, [FromBody] Purchase purchase)
        {
            repPurchase.UpdatePurchase(purchase);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [TypeFilter(typeof(Purchase_ValidatePurchaseIdFilterAttribute))]
        public IActionResult Delete(int id)
        {
            repPurchase.DeletePurchase(id);

            return Ok(HttpContext.Items["purchase"]);
        }
    }
}
