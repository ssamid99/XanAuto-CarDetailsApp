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
    public class CurrencyGetAllQuery : IRequest<List<Currency>>
    {
        public class CurrencyGetAllQueryHandler : IRequestHandler<CurrencyGetAllQuery, List<Currency>>
        {
            private readonly XanAutoDbContext db;

            public CurrencyGetAllQueryHandler(XanAutoDbContext db)
            {
                this.db = db;
            }
            public async Task<List<Currency>> Handle(CurrencyGetAllQuery request, CancellationToken cancellationToken)
            {
                var query = await db.Currencies.Where(c => c.DeletedDate == null).ToListAsync(cancellationToken);

                if (query == null)
                {
                    return null;
                }

                return query;
            }
        }

    }
}
