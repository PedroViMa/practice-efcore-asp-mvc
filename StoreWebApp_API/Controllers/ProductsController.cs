using Microsoft.AspNetCore.Mvc;
using StoreWebApp_API.Filters.ActionFilters.ProductValidations;
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
    public class ProductsController : ControllerBase
    {
        private readonly IRepProduct repProduct;

        public ProductsController(StoreDbContext db)
        {
            repProduct = new AdoProductRep();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(repProduct.GetProducts());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(Product_ValidateProductIdFilterAttribute))]
        public IActionResult Details(int id)
        {
            return Ok(repProduct.GetProductById(id));
        }

        [HttpPost]
        [Product_ValidateCreateProductFilter]
        public IActionResult Create([FromBody] Product product)
        {
            repProduct.CreateProduct(product);

            return CreatedAtAction(nameof(Details),
                new { id = product.Id },
                product);
        }

        [HttpPut("{id}")]
        [TypeFilter(typeof(Product_ValidateProductIdFilterAttribute))]
        [Product_ValidateUpdateProductFilter]
        [TypeFilter(typeof(Product_HandleUpdateExceptionsFilterAttribute))]
        public IActionResult Edit(int id, [FromBody] Product product)
        {
            repProduct.UpdateProduct(product);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [TypeFilter(typeof(Product_ValidateProductIdFilterAttribute))]
        public IActionResult Delete(int id)
        {
            repProduct.DeleteProduct(id);

            return Ok(HttpContext.Items["product"]);
        }
    }
}
