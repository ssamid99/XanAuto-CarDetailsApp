using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XanAuto.Domain.Models.DbContexts;
using XanAuto.Domain.Models.Entities;

namespace XanAuto.Domain.Business.GroupModule
{
    public class GroupRemoveCommand : IRequest<Group>
    {
        public int Id { get; set; }
        public class GroupRemoveCommandHandler : IRequestHandler<GroupRemoveCommand, Group>
        {
            private readonly XanAutoDbContext db;

            public GroupRemoveCommandHandler(XanAutoDbContext db)
            {
                this.db = db;
            }
            public async Task<Group> Handle(GroupRemoveCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Groups.FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedDate == null, cancellationToken);
                if (data == null)
                {
                    return null;
                }
                data.DeletedDate = DateTime.UtcNow.AddHours(4);
                return data;
            }
        }
    }
}
