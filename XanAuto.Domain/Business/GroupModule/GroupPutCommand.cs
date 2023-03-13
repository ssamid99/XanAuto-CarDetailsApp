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
    public class GroupPutCommand : IRequest<Group>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public class GroupPutCommandHandler : IRequestHandler<GroupPutCommand, Group>
        {
            private readonly XanAutoDbContext db;

            public GroupPutCommandHandler(XanAutoDbContext db)
            {
                this.db = db;
            }
            public async Task<Group> Handle(GroupPutCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Groups.FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedDate == null, cancellationToken);
                if (data == null)
                {
                    return null;
                }
                data.Name = request.Name;
                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
