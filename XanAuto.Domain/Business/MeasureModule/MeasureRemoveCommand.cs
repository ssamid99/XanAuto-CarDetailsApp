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
    public class MeasureRemoveCommand : IRequest<Measure>
    {
        public int Id { get; set; }
        public class MeasureRemoveCommandHandler : IRequestHandler<MeasureRemoveCommand, Measure>
        {
            private readonly XanAutoDbContext db;

            public MeasureRemoveCommandHandler(XanAutoDbContext db)
            {
                this.db = db;
            }
            public async Task<Measure> Handle(MeasureRemoveCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Measures.FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedDate == null, cancellationToken);
                if (data == null)
                {
                    return null;
                }
                data.DeletedDate = DateTime.UtcNow.AddHours(4);
                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
