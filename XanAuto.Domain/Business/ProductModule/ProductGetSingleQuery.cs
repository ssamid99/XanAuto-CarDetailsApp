using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XanAuto.Domain.AppCode.Infrastructure;
using XanAuto.Domain.Models.DbContexts;
using XanAuto.Domain.Models.Entities;

namespace XanAuto.Domain.Business.ProductModule
{
    public class ProductGetSingleQuery : IRequest<Product>
    {
        public int Id { get; set; }
        public class ProductGetSingleQueryHandler : IRequestHandler<ProductGetSingleQuery, Product>
        {
            private readonly XanAutoDbContext db;

            public ProductGetSingleQueryHandler(XanAutoDbContext db)
            {
                this.db = db;
            }
            public async Task<Product> Handle(ProductGetSingleQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Products.FirstOrDefaultAsync(p => p.Id == request.Id && p.DeletedDate == null, cancellationToken);
                if(data == null)
                {
                    return null;
                }
                return data;
            }
        }
    }
}
