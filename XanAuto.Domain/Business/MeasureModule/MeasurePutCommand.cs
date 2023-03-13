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
    public class MeasurePutCommand : IRequest<Measure>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public class MeasurePutCommandHandler : IRequestHandler<MeasurePutCommand, Measure>
        {
            private readonly XanAutoDbContext db;

            public MeasurePutCommandHandler(XanAutoDbContext db)
            {
                this.db = db;
            }
            public async Task<Measure> Handle(MeasurePutCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Measures.FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedDate == null, cancellationToken);
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
