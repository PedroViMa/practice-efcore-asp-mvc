using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreWebApp_DAL.Data;
using StoreWebApp_Model.Models;
using StoreWebApp_DAL.DAO.Interfaces;
using StoreWebApp_DAL.DAO.EFCRep;

namespace StoreWebApp.Controllers
{
    public class SalesController : Controller
    {
        private readonly IRepSale _repSale;
        private readonly IRepProduct _repProduct;

        public SalesController(StoreDbContext context)
        {
            _repProduct = new EFCProductRep(context);
            _repSale = new EFCSaleRep(context);
        }

        // GET: Sales
        public IActionResult Index()
        {
            return View(_repSale.GetSales());
        }

        // GET: Sales/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = _repSale.GetSaleById(id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_repProduct.GetProducts(), "Id", "Name");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Quantity,Date,Price,ProductId")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                _repSale.CreateSale(sale);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_repProduct.GetProducts(), "Id", "Name", sale.ProductId);
            return View(sale);
        }

        // GET: Sales/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = _repSale.GetSaleById(id);
            if (sale == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_repProduct.GetProducts(), "Id", "Name", sale.ProductId);
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Quantity,Date,Price,ProductId")] Sale sale)
        {
            if (id != sale.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repSale.UpdateSale(sale);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleExists(sale.Id))
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
            ViewData["ProductId"] = new SelectList(_repProduct.GetProducts(), "Id", "Name", sale.ProductId);
            return View(sale);
        }

        // GET: Sales/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = _repSale.GetSaleById(id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repSale.DeleteSale(id);
            return RedirectToAction(nameof(Index));
        }

        private bool SaleExists(int id)
        {
            return _repSale.GetSaleById(id)
                != null;
        }
    }
}
