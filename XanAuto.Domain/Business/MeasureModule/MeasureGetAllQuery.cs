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

namespace XanAuto.Domain.Business.MeasureModule
{
    public class MeasureGetAllQuery : IRequest<List<Measure>>
    {
        public class MeasureGetAllQueryHandler : IRequestHandler<MeasureGetAllQuery, List<Measure>>
        {
            private readonly XanAutoDbContext db;

            public MeasureGetAllQueryHandler(XanAutoDbContext db)
            {
                this.db = db;
            }
            public async Task<List<Measure>> Handle(MeasureGetAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Measures.Where(m => m.DeletedDate == null).ToListAsync(cancellationToken);
                if (data == null)
                {
                    return null;
                }
                return data;
            }
        }
    }
}
