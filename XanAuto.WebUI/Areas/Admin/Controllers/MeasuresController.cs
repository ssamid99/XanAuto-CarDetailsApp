using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using XanAuto.Domain.Business.MeasureModule;
using XanAuto.Domain.Models.DbContexts;
using XanAuto.Domain.Models.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace XanAuto.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MeasuresController : Controller
    {
        private readonly XanAutoDbContext db;
        private readonly IMediator mediator;

        public MeasuresController(XanAutoDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }

        // GET: Admin/Measures
        [Authorize("admin.measures.index")]
        public async Task<IActionResult> Index(MeasureGetAllQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }

        // GET: Admin/Measures/Create
        [Authorize("admin.measures.create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Measures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("admin.measures.create")]
        public async Task<IActionResult> Create(MeasurePostCommand command)
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
        [Authorize("admin.measures.edit")]
        // GET: Admin/Measures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measure = await db.Measures.FindAsync(id);
            if (measure == null)
            {
                return NotFound();
            }
            var editCommand = new MeasurePutCommand();
            editCommand.Name = measure.Name;
            return View(editCommand);
        }

        // POST: Admin/Measures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("admin.measures.edit")]
        public async Task<IActionResult> Edit(MeasurePutCommand command)
        {
            var response = await mediator.Send(command);

            if (response != null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(command);
        }

        // POST: Admin/Measures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize("admin.measures.delete")]
        public async Task<IActionResult> DeleteConfirmed(MeasureRemoveCommand command)
        {
            var response = await mediator.Send(command);
            if (response == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MeasureExists(int id)
        {
            return db.Measures.Any(e => e.Id == id);
        }
    }
}
