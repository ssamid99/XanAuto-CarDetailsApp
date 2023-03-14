using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using XanAuto.Domain.Business.GroupModule;
using XanAuto.Domain.Models.DbContexts;
using XanAuto.Domain.Models.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace XanAuto.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GroupsController : Controller
    {
        private readonly XanAutoDbContext db;
        private readonly IMediator mediator;

        public GroupsController(XanAutoDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }
        [Authorize("admin.groups.index")]
        // GET: Admin/Groups
        public async Task<IActionResult> Index(GroupGetAllQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }

        [Authorize("admin.groups.create")]
        // GET: Admin/Groups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("admin.groups.create")]
        public async Task<IActionResult> Create(GroupPostCommand command)
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
        [Authorize("admin.groups.edit")]
        // GET: Admin/Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await db.Groups.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            var editCommand = new GroupPutCommand();
            editCommand.Name = entity.Name;
            return View(editCommand);
        }

        // POST: Admin/Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("admin.groups.edit")]
        public async Task<IActionResult> Edit(GroupPutCommand command)
        {
            var response = await mediator.Send(command);

            if (response != null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(command);
        }
        // POST: Admin/Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize("admin.groups.delete")]
        public async Task<IActionResult> DeleteConfirmed(GroupRemoveCommand command)
        {
            var response = await mediator.Send(command);
            if (response == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {
            return db.Groups.Any(e => e.Id == id);
        }
    }
}
