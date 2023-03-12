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
    public class CurrencyPutCommand : IRequest<Currency>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public class CurrencyPutCommandHandler : IRequestHandler<CurrencyPutCommand, Currency>
        {
            private readonly XanAutoDbContext db;

            public CurrencyPutCommandHandler(XanAutoDbContext db)
            {
                this.db = db;
            }
            public async Task<Currency> Handle(CurrencyPutCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Currencies.FirstOrDefaultAsync(c => c.Id == request.Id && c.DeletedDate == null, cancellationToken);

                if(data == null)
                {
                    return null;
                }

                data.Name = request.Name;
                data.Code = request.Code;
                
                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
