using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreWebApp_Model.Models;
using StoreWebApp.Data;

namespace StoreWebApp.Controllers
{
    public class SalesController : Controller
    {
        private readonly IWebApiExecutor webApiExecutor;

        public SalesController(IWebApiExecutor webApiExecutor)
        {
            this.webApiExecutor = webApiExecutor;
        }

        public async Task<IActionResult> Index()
        {
            return View(await webApiExecutor.InvokeGet<List<Sale>>("sales"));
        }

        public async Task<IActionResult> Details(int? id)
        {
            var sale = await webApiExecutor.InvokeGet<Sale>($"sales/{id}");
            if (sale == null)
            {
                return NotFound();
            }
            return View(sale);
        }

        public async Task<IActionResult> Create()
        {
            var products = await webApiExecutor.InvokeGet<List<Product>>("products");
            ViewData["ProductId"] = new SelectList(products, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sale sale)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await webApiExecutor.InvokePost("sales", sale);
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

            var products = await webApiExecutor.InvokeGet<List<Product>>("products");
            ViewData["ProductId"] = new SelectList(products, "Id", "Name", sale.ProductId);

            return View(sale);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                var sale = await webApiExecutor.InvokeGet<Sale>($"sales/{id}");
                if (sale != null)
                {
                    var products = await webApiExecutor.InvokeGet<List<Product>>("products");
                    ViewData["ProductId"] = new SelectList(products, "Id", "Name", sale.ProductId);

                    return View(sale);
                }
            }
            catch (WebApiException e)
            {
                HandleWebApiException(e);
                return View();
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Sale sale)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await webApiExecutor.InvokePut($"sales/{sale.Id}", sale);
                    return RedirectToAction(nameof(Index));
                }
                catch (WebApiException e)
                {
                    HandleWebApiException(e);
                }
            }

            var products = await webApiExecutor.InvokeGet<List<Product>>("products");
            ViewData["ProductId"] = new SelectList(products, "Id", "Name", sale.ProductId);

            return View(sale);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var sale = await webApiExecutor.InvokeGet<Sale>($"sales/{id}");
                if (sale != null)
                {
                    return View(sale);
                }
            }
            catch (WebApiException e)
            {
                HandleWebApiException(e);
                return View();
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await webApiExecutor.InvokeDelete($"sales/{id}");
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
