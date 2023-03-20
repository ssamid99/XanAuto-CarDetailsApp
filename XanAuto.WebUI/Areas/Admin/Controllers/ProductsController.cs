using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using XanAuto.Domain.Business.ProductModule;
using XanAuto.Domain.Models.DbContexts;
using XanAuto.Domain.Models.Entities;

namespace XanAuto.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly XanAutoDbContext db;
        private readonly IMediator mediator;

        public ProductsController(XanAutoDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }

        // GET: Admin/Products
        [Authorize("admin.products.index")]
        public async Task<IActionResult> Index(ProductGetAllQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }

        // GET: Admin/Products/Details/5
        [Authorize("admin.products.details")]
        public async Task<IActionResult> Details(ProductGetSingleQuery query)
        {
            var response = await mediator.Send(query);
            if(response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        // GET: Admin/Products/Create
        [Authorize("admin.products.create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Currency = await db.Currencies.Where(c => c.DeletedDate == null).ToListAsync();
            ViewBag.Measure = await db.Measures.Where(m => m.DeletedDate == null).ToListAsync();
            ViewBag.Supplier = await db.Suppliers.Where(s => s.DeletedDate == null).ToListAsync();
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("admin.products.create")]
        public async Task<IActionResult> Create(ProductPostCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", command);
            }
            else
            {
                ViewBag.Currency = await db.Currencies.Where(c => c.DeletedDate == null).ToListAsync();
                ViewBag.Measure = await db.Measures.Where(m => m.DeletedDate == null).ToListAsync();
                ViewBag.Supplier = await db.Suppliers.Where(s => s.DeletedDate == null).ToListAsync();
                var reponse = await mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Admin/Products/Edit/5
        [Authorize("admin.products.edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var editCommand = new ProductPutCommand();
            editCommand.Code = product.Code;
            editCommand.Brand = product.Brand;
            editCommand.Oe = product.Oe;
            editCommand.Name = product.Name;
            editCommand.Important = product.Important;
            editCommand.Discount = product.Discount;
            editCommand.Active = product.Active;
            editCommand.Original = product.Original;
            editCommand.Amount = product.Amount;
            editCommand.Norm = product.Norm;
            editCommand.Order = product.Order;
            editCommand.Cost = product.Cost;
            editCommand.SellingPrice = product.SellingPrice;
            editCommand.RetailPrice = product.RetailPrice;
            editCommand.Tag = product.Tag;
            editCommand.Description = product.Description;
            editCommand.ImagePath = product.ImagePath;

            var catalog = await db.ProductCatalogItem.FirstOrDefaultAsync(p => p.ProductId == product.Id && p.DeletedDate == null);
            if(catalog == null)
            {
                return NotFound();
            }
            editCommand.MeasureId = catalog.MeasureId;
            editCommand.CurrencyId = catalog.CurrencyId;
            editCommand.SupplierId = catalog.SupplierId;
            ViewBag.Currency = await db.Currencies.Where(c => c.DeletedDate == null).ToListAsync();
            ViewBag.Measure = await db.Measures.Where(m => m.DeletedDate == null).ToListAsync();
            ViewBag.Supplier = await db.Suppliers.Where(s => s.DeletedDate == null).ToListAsync();
            return View(editCommand);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("admin.products.edit")]
        public async Task<IActionResult> Edit(ProductPutCommand command)
        {
            var response = await mediator.Send(command);

            if (response != null)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Currency = await db.Currencies.Where(c => c.DeletedDate == null).ToListAsync();
            ViewBag.Measure = await db.Measures.Where(m => m.DeletedDate == null).ToListAsync();
            ViewBag.Supplier = await db.Suppliers.Where(s => s.DeletedDate == null).ToListAsync();
            return View(command);
        }


        // POST: Admin/Products/Delete/5
        [Authorize("admin.products.delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ProductRemoveCommand command)
        {
            var response = await mediator.Send(command);
            if (response == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return db.Products.Any(e => e.Id == id);
        }
    }
}
