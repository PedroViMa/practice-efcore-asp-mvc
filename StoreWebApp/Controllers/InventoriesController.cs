using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreWebApp_DAL.DAO.Interfaces;
using StoreWebApp_DAL.DAO.EFCRep;
using StoreWebApp_DAL.Data;
using StoreWebApp_Model.Models;

namespace StoreWebApp.Controllers
{
    public class InventoriesController : Controller
    {
        private readonly IRepInventory _repInventory;
        private readonly IRepProduct _repProduct;

        public InventoriesController(StoreDbContext context)
        {
            _repInventory = new EFCInventoryRep(context);
            _repProduct = new EFCProductRep(context);
        }

        // GET: Inventories
        public IActionResult Index()
        {
            return View(_repInventory.GetInventories());
        }

        // GET: Inventories/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = _repInventory.GetInventoryById(id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // GET: Inventories/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_repProduct.GetProducts(), "Id", "Name");
            return View();
        }

        // POST: Inventories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Quantity,Date,ProductId")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _repInventory.CreateInventory(inventory);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_repProduct.GetProducts(), "Id", "Name", inventory.ProductId);
            return View(inventory);
        }

        // GET: Inventories/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = _repInventory.GetInventoryById(id);
            if (inventory == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_repProduct.GetProducts(), "Id", "Name", inventory.ProductId);
            return View(inventory);
        }

        // POST: Inventories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Quantity,Date,ProductId")] Inventory inventory)
        {
            if (id != inventory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repInventory.UpdateInventory(inventory);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryExists(inventory.Id))
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
            ViewData["ProductId"] = new SelectList(_repProduct.GetProducts(), "Id", "Name", inventory.ProductId);
            return View(inventory);
        }

        // GET: Inventories/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = _repInventory.GetInventoryById(id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repInventory.DeleteInventory(id);
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryExists(int id)
        {
            return _repInventory.GetInventoryById(id)
                != null;
        }
    }
}
