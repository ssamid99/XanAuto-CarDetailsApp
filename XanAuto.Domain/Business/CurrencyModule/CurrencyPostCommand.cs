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

namespace XanAuto.Domain.Business.CurrencyModule
{
    public class CurrencyPostCommand : IRequest<Currency>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public class CurrencyPostCommandHandler : IRequestHandler<CurrencyPostCommand, Currency>
        {
            private readonly XanAutoDbContext db;
            private readonly IActionContextAccessor ctx;

            public CurrencyPostCommandHandler(XanAutoDbContext db, IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }
            public async Task<Currency> Handle(CurrencyPostCommand request, CancellationToken cancellationToken)
            {
                var entity = new Currency();
                entity.Name = request.Name;
                entity.Code = request.Code;
                entity.CreatedByUserId = ctx.GetCurrentUserId();
                await db.Currencies.AddAsync(entity, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);
                return entity;
            }
        }
    }
}
