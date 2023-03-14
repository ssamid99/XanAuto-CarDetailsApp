using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using XanAuto.Domain.Business.ModelModule;
using XanAuto.Domain.Models.DbContexts;
using XanAuto.Domain.Models.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace XanAuto.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ModelsController : Controller
    {
        private readonly XanAutoDbContext db;
        private readonly IMediator mediator;

        public ModelsController(XanAutoDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }

        // GET: Admin/Models
        [Authorize("admin.models.index")]
        public async Task<IActionResult> Index(ModelGetAllQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }

        // GET: Admin/Models/Create
        [Authorize("admin.models.create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Models/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("admin.models.create")]
        public async Task<IActionResult> Create(ModelPostCommand command)
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

        // GET: Admin/Models/Edit/5
        [Authorize("admin.models.edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await db.Models.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            var editCommand = new ModelPutCommand();
            editCommand.Name = model.Name;
            return View(editCommand);
        }

        // POST: Admin/Models/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("admin.models.edit")]
        public async Task<IActionResult> Edit(ModelPutCommand command)
        {
            var response = await mediator.Send(command);

            if (response != null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(command);
        }

        // POST: Admin/Models/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize("admin.models.delete")]
        public async Task<IActionResult> DeleteConfirmed(ModelRemoveCommand command)
        {
            var response = await mediator.Send(command);
            if (response == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ModelExists(int id)
        {
            return db.Models.Any(e => e.Id == id);
        }
    }
}
