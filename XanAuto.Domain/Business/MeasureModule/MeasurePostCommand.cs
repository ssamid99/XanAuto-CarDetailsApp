using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XanAuto.Application.AppCode.Extensions;
using XanAuto.Domain.Models.DbContexts;
using XanAuto.Domain.Models.Entities;

namespace XanAuto.Domain.Business.MeasureModule
{
    public class MeasurePostCommand : IRequest<Measure>
    {
        public string Name { get; set; }
        public class MeasurePostCommandHandler : IRequestHandler<MeasurePostCommand, Measure>
        {
            private readonly XanAutoDbContext db;
            private readonly IActionContextAccessor ctx;

            public MeasurePostCommandHandler(XanAutoDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }
            public async Task<Measure> Handle(MeasurePostCommand request, CancellationToken cancellationToken)
            {
                var entity = new Measure();
                entity.Name = request.Name;
                entity.CreatedByUserId = ctx.GetCurrentUserId();
                await db.Measures.AddAsync(entity, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);
                return entity;
            }
        }
    }
}
