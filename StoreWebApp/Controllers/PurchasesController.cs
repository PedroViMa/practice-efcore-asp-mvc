using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreWebApp.Data;
using StoreWebApp_Model.Models;

namespace StoreWebApp.Controllers
{
    public class PurchasesController : Controller
    {
        private readonly IWebApiExecutor webApiExecutor;

        public PurchasesController(IWebApiExecutor webApiExecutor)
        {
            this.webApiExecutor = webApiExecutor;
        }

        // GET: Purchases
        public async Task<IActionResult> Index()
        {
            return View(await webApiExecutor.InvokeGet<List<Purchase>>("purchases"));
        }

        // GET: Purchases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var purchase = await webApiExecutor.InvokeGet<Purchase>($"purchases/{id}");
            if (purchase == null)
            {
                return NotFound();
            }
            return View(purchase);
        }

        // GET: Purchases/Create
        public async Task<IActionResult> Create()
        {
            var products = await webApiExecutor.InvokeGet<List<Product>>("products");
            ViewData["ProductId"] = new SelectList(products, "Id", "Name");
            return View();
        }

        // POST: Purchases/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await webApiExecutor.InvokePost("purchases", purchase);
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
            ViewData["ProductId"] = new SelectList(products, "Id", "Name", purchase.ProductId);
            
            return View(purchase);
        }

        // GET: Purchases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                var purchase = await webApiExecutor.InvokeGet<Purchase>($"purchases/{id}");
                if (purchase != null)
                {
                    var products = await webApiExecutor.InvokeGet<List<Product>>("products");
                    ViewData["ProductId"] = new SelectList(products, "Id", "Name", purchase.ProductId);

                    return View(purchase);
                }
            }
            catch (WebApiException e)
            {
                HandleWebApiException(e);
                return View();
            }

            return NotFound();
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await webApiExecutor.InvokePut($"purchases/{purchase.Id}", purchase);
                    return RedirectToAction(nameof(Index));
                }
                catch (WebApiException e)
                {
                    HandleWebApiException(e);
                }
            }

            var products = await webApiExecutor.InvokeGet<List<Product>>("products");
            ViewData["ProductId"] = new SelectList(products, "Id", "Name", purchase.ProductId);
            
            return View(purchase);
        }

        // GET: Purchases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var product = await webApiExecutor.InvokeGet<Purchase>($"purchases/{id}");
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

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await webApiExecutor.InvokeDelete($"purchases/{id}");
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
