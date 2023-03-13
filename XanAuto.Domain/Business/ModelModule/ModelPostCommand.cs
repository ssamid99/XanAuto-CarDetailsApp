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

namespace XanAuto.Domain.Business.ModelModule
{
    public class ModelPostCommand : IRequest<Model>
    {
        public string Name { get; set; }
        public class ModelPostCommandHandler : IRequestHandler<ModelPostCommand, Model>
        {
            private readonly XanAutoDbContext db;
            private readonly IActionContextAccessor ctx;

            public ModelPostCommandHandler(XanAutoDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }
            public async Task<Model> Handle(ModelPostCommand request, CancellationToken cancellationToken)
            {
                var entity = new Model();
                entity.Name = request.Name;
                entity.CreatedByUserId = ctx.GetCurrentUserId();
                await db.Models.AddAsync(entity, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);
                return entity;
            }
        }
    }
}
