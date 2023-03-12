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

namespace XanAuto.Domain.Business.SupplierModule
{
    public class SupplierRemoveCommand : IRequest<Supplier>
    {
        public int Id { get; set; }
        public class SupplierRemoveCommandHandler : IRequestHandler<SupplierRemoveCommand, Supplier>
        {
            private readonly XanAutoDbContext db;

            public SupplierRemoveCommandHandler(XanAutoDbContext db)
            {
                this.db = db;
            }
            public async Task<Supplier> Handle(SupplierRemoveCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Suppliers.FirstOrDefaultAsync(s => s.Id == request.Id && s.DeletedDate == null, cancellationToken);
                if(data == null)
                {
                    return null;
                }
                data.DeletedDate = DateTime.UtcNow.AddHours(4);
                return data;
            }
        }
    }
}
