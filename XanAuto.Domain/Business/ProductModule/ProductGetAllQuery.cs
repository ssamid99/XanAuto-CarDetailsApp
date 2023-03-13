using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XanAuto.Application.AppCode.Infrastructure;
using XanAuto.Domain.Models.DbContexts;
using XanAuto.Domain.Models.Entities;

namespace XanAuto.Domain.Business.ProductModule
{
    public class ProductGetAllQuery : PaginateModel, IRequest<PagedViewModel<Product>>
    {
        public class ProductGetAllQueryHandler : IRequestHandler<ProductGetAllQuery, PagedViewModel<Product>>
        {
            private readonly XanAutoDbContext db;

            public ProductGetAllQueryHandler(XanAutoDbContext db)
            {
                this.db = db;
            }
            public async Task<PagedViewModel<Product>> Handle(ProductGetAllQuery request, CancellationToken cancellationToken)
            {
                if (request.PageSize < 12)
                {
                    request.PageSize = 12;
                }
                var query = db.Products
                    .Where(r => r.DeletedDate == null)
                    .AsQueryable();
                if (query == null)
                {
                    return null;
                }
                var pagedModel = new PagedViewModel<Product>(query, request);
                return pagedModel;
            }
        }
    }
}
