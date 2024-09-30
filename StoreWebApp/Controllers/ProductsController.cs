using Microsoft.AspNetCore.Mvc;
using StoreWebApp.Data;
using StoreWebApp_Model.Models;

namespace StoreWebApp.Controllers
{
    public class ProductsController : Controller
    {
        //private readonly IRepProduct _repProduct;
        private readonly IWebApiExecutor webApiExecutor;

        public ProductsController(IWebApiExecutor webApiExecutor)
        {
            //_repProduct = new EFCProductRep(db);
            //_repProduct = new AdoProductRep();
            this.webApiExecutor = webApiExecutor;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await webApiExecutor.InvokeGet<List<Product>>("products"));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await webApiExecutor.InvokeGet<Product>($"products/{id}");
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await webApiExecutor.InvokePost("products", product);
                    if (response != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (WebApiException e)
                {
                    HandleWebApiException(e);
                }
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var product = await webApiExecutor.InvokeGet<Product>($"products/{id}");
                if (product != null)
                {
                    return View(product);
                }
            }
            catch (WebApiException e)
            {
                HandleWebApiException(e);
                return View();
            }

            return NotFound();
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await webApiExecutor.InvokePut($"products/{product.Id}", product);
                    return RedirectToAction(nameof(Index));
                }
                catch (WebApiException e)
                {
                    HandleWebApiException(e);
                }
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await webApiExecutor.InvokeGet<Product>($"products/{id}");
                if (product != null)
                {
                    return View(product);
                }
            }
            catch (WebApiException e)
            {
                HandleWebApiException(e);
                return View();
            }
            return NotFound();
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await webApiExecutor.InvokeDelete($"products/{id}");
                return RedirectToAction(nameof(Index));
            }
            catch (WebApiException ex)
            {
                HandleWebApiException(ex);
                return View();
            }
        }

        private void HandleWebApiException(WebApiException e)
        {
            if (e.Response != null &&
                e.Response.Errors != null &&
                e.Response.Errors.Count > 0)
            {
                foreach (var error in e.Response.Errors)
                {
                    ModelState.AddModelError(error.Key, string.Join(";", error.Value));
                }
            }
            else if (e.Response != null)
            {
                ModelState.AddModelError("Error", e.Response.Title);
            }
            else
            {
                ModelState.AddModelError("Error", e.Message);
            }
        }
    }
}
