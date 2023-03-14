using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using XanAuto.Domain.Business.CurrencyModule;
using XanAuto.Domain.Models.DbContexts;
using XanAuto.Domain.Models.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace XanAuto.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CurrenciesController : Controller
    {
        private readonly XanAutoDbContext db;
        private readonly IMediator mediator;

        public CurrenciesController(XanAutoDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }

        [Authorize("admin.currencies.index")]
        // GET: Admin/Currencies
        public async Task<IActionResult> Index(CurrencyGetAllQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }

        [Authorize("admin.currencies.create")]
        // GET: Admin/Currencies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Currencies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("admin.currencies.create")]
        public async Task<IActionResult> Create(CurrencyPostCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", command);
            }
            else
            {
                var reponse = await mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Admin/Currencies/Edit/5
        [Authorize("admin.currencies.edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await db.Currencies.FindAsync(id);
            if (currency == null)
            {
                return NotFound();
            }
            var editCommand = new CurrencyPutCommand();
            editCommand.Name = currency.Name;
            return View(editCommand);
        }

        // POST: Admin/Currencies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("admin.currencies.edit")]
        public async Task<IActionResult> Edit(CurrencyPutCommand command)
        {
            var response = await mediator.Send(command);

            if (response != null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(command);
        }
        // POST: Admin/Currencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize("admin.currencies.delete")]
        public async Task<IActionResult> DeleteConfirmed(CurrencyRemoveCommand command)
        {
            var response = await mediator.Send(command);
            if (response == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CurrencyExists(int id)
        {
            return db.Currencies.Any(e => e.Id == id);
        }
    }
}
