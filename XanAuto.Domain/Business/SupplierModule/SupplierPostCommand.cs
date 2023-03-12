using MediatR;
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
    public class SupplierPostCommand : IRequest<Supplier>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public double Loan { get; set; }
        public int CurrencyId { get; set; }
        public class SupplierPostCommandHandler : IRequestHandler<SupplierPostCommand, Supplier>
        {
            private readonly XanAutoDbContext db;

            public SupplierPostCommandHandler(XanAutoDbContext db)
            {
                this.db = db;
            }
            public async Task<Supplier> Handle(SupplierPostCommand request, CancellationToken cancellationToken)
            {
                var entity = new Supplier();
                entity.Name = request.Name;
                entity.Surname = request.Surname;
                entity.Loan = request.Loan;
                entity.CurrencyId = request.CurrencyId;
                await db.Suppliers.AddAsync(entity, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);
                return entity;
            }
        }
    }
}
