using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XanAuto.Application.AppCode.Infrastructure;
using XanAuto.Domain.Models.DbContexts;
using XanAuto.Domain.Models.Entities;

namespace XanAuto.Domain.Business.SupplierModule
{
    public class SupplierGetAllQuery : PaginateModel, IRequest<PagedViewModel<Supplier>>
    {
        public class SupplierGetAllQueryHandler : IRequestHandler<SupplierGetAllQuery, PagedViewModel<Supplier>>
        {
            private readonly XanAutoDbContext db;

            public SupplierGetAllQueryHandler(XanAutoDbContext db)
            {
                this.db = db;
            }
            public async Task<PagedViewModel<Supplier>> Handle(SupplierGetAllQuery request, CancellationToken cancellationToken)
            {
                if (request.PageSize < 12)
                {
                    request.PageSize = 12;
                }
                var query = db.Suppliers
                    .Where(r => r.DeletedDate == null)
                    .AsQueryable();
                if (query == null)
                {
                    return null;
                }
                var pagedModel = new PagedViewModel<Supplier>(query, request);
                return pagedModel;
            }
        }
    }
}
