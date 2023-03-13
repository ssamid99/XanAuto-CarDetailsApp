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
    public class GroupGetAllQuery : IRequest<List<Group>>
    {
        public class GroupGetAllQueryHandler : IRequestHandler<GroupGetAllQuery, List<Group>>
        {
            private readonly XanAutoDbContext db;

            public GroupGetAllQueryHandler(XanAutoDbContext db)
            {
                this.db = db;
            }
            public async Task<List<Group>> Handle(GroupGetAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Groups.Where(m => m.DeletedDate == null).ToListAsync(cancellationToken);
                if (data == null)
                {
                    return null;
                }
                return data;
            }
        }
    }
}
