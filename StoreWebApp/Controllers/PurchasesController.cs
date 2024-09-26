using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreWebApp_DAL.DAO.AdoRep;
using StoreWebApp_DAL.DAO.EFCRep;
using StoreWebApp_DAL.DAO.Interfaces;
using StoreWebApp_DAL.Data;
using StoreWebApp_Model.Models;

namespace StoreWebApp.Controllers
{
    public class PurchasesController : Controller
    {
        private readonly IRepPurchase _repPurchase;
        private readonly IRepProduct _repProduct;

        public PurchasesController(StoreDbContext context)
        {
            //_repProduct = new EFCProductRep(context);
            //_repPurchase = new EFCPurchaseRep(context);
            _repProduct = new AdoProductRep();
            _repPurchase = new AdoPurchaseRep();
        }

        // GET: Purchases
        public IActionResult Index()
        {
            return View(_repPurchase.GetPurchases());
        }

        // GET: Purchases/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = _repPurchase.GetPurchaseById(id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // GET: Purchases/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_repProduct.GetProducts(), "Id", "Name");
            return View();
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Quantity,Date,Price,ProductId")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                _repPurchase.CreatePurchase(purchase);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_repProduct.GetProducts(), "Id", "Name", purchase.ProductId);
            return View(purchase);
        }

        // GET: Purchases/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = _repPurchase.GetPurchaseById(id);
            if (purchase == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_repProduct.GetProducts(), "Id", "Name", purchase.ProductId);
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Quantity,Date,Price,ProductId")] Purchase purchase)
        {
            if (id != purchase.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repPurchase.UpdatePurchase(purchase);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseExists(purchase.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_repProduct.GetProducts(), "Id", "Name", purchase.ProductId);
            return View(purchase);
        }

        // GET: Purchases/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = _repPurchase.GetPurchaseById(id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repPurchase.DeletePurchase(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseExists(int id)
        {
            return _repPurchase.GetPurchaseById(id)
                != null;
        }
    }
}
