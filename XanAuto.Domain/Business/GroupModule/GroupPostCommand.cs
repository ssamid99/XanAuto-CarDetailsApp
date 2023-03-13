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

namespace XanAuto.Domain.Business.GroupModule
{
    public class GroupPostCommand : IRequest<Group>
    {
        public string Name { get; set; }
        public class GroupPostCommandHandler : IRequestHandler<GroupPostCommand, Group>
        {
            private readonly XanAutoDbContext db;
            private readonly IActionContextAccessor ctx;

            public GroupPostCommandHandler(XanAutoDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }
            public async Task<Group> Handle(GroupPostCommand request, CancellationToken cancellationToken)
            {
                var entity = new Group();
                entity.Name = request.Name;
                entity.CreatedByUserId = ctx.GetCurrentUserId();
                await db.Groups.AddAsync(entity, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);
                return entity;
            }
        }
    }
}
