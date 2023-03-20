using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using XanAuto.Domain.Business.SupplierModule;
using XanAuto.Domain.Models.DbContexts;

namespace XanAuto.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SuppliersController : Controller
    {
        private readonly XanAutoDbContext db;
        private readonly IMediator mediator;

        public SuppliersController(XanAutoDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }

        // GET: Admin/Suppliers
        [Authorize("admin.suppliers.index")]
        public async Task<IActionResult> Index(SupplierGetAllQuery query)
        {
            var response = await mediator.Send(query);
            ViewBag.GetCName = new Func<int, string>(GetCName);
            return View(response);
        }


        // GET: Admin/Suppliers/Create
        [Authorize("admin.suppliers.create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Currency = await db.Currencies.Where(c => c.DeletedDate == null).ToListAsync();
            return View();
        }

        // POST: Admin/Suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("admin.suppliers.create")]
        public async Task<IActionResult> Create(SupplierPostCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", command);
            }
            else
            {
                ViewBag.Currency = await db.Currencies.Where(c => c.DeletedDate == null).ToListAsync();
                var reponse = await mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Admin/Suppliers/Edit/5
        [Authorize("admin.suppliers.edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await db.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            var editCommand = new SupplierPutCommand();
            editCommand.Name = supplier.Name;
            editCommand.Surname = supplier.Surname;
            editCommand.Loan = supplier.Loan;
            editCommand.CurrencyId = supplier.CurrencyId;

            ViewBag.Currency = await db.Currencies.Where(c => c.DeletedDate == null).ToListAsync();
            return View(editCommand);
        }

        // POST: Admin/Suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("admin.suppliers.edit")]
        public async Task<IActionResult> Edit(SupplierPutCommand command)
        {
            var response = await mediator.Send(command);

            if (response != null)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Currency = await db.Currencies.Where(c => c.DeletedDate == null).ToListAsync();
            return View(command);
        }

        // POST: Admin/Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize("admin.suppliers.delete")]
        public async Task<IActionResult> DeleteConfirmed(SupplierRemoveCommand command)
        {
            var response = await mediator.Send(command);
            if (response == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        public string GetCName(int id)
        {
            var data = db.Currencies.FirstOrDefault(c => c.Id == id && c.DeletedDate == null);
            return data.Code;
        }
    }
}
