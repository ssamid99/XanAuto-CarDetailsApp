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

namespace XanAuto.Domain.Business.CurrencyModule
{
    public class CurrencyRemoveCommand : IRequest<Currency>
    {
        public int Id { get; set; }
        public class CurrencyRemoveCommandHandler : IRequestHandler<CurrencyRemoveCommand, Currency>
        {
            private readonly XanAutoDbContext db;

            public CurrencyRemoveCommandHandler(XanAutoDbContext db)
            {
                this.db = db;
            }
            public async Task<Currency> Handle(CurrencyRemoveCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Currencies.FirstOrDefaultAsync(c => c.Id == request.Id && c.DeletedDate == null, cancellationToken);

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
