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
    public class SupplierPutCommand : IRequest<Supplier>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public double Loan { get; set; }
        public int CurrencyId { get; set; }
        public class SupplierPutCommandHandler : IRequestHandler<SupplierPutCommand, Supplier>
        {
            private readonly XanAutoDbContext db;

            public SupplierPutCommandHandler(XanAutoDbContext db)
            {
                this.db = db;
            }
            public async Task<Supplier> Handle(SupplierPutCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Suppliers.FirstOrDefaultAsync(s => s.Id == request.Id && s.DeletedDate == null, cancellationToken);
                if(data == null)
                {
                    return null;
                }
                data.Name = request.Name;
                data.Surname = request.Surname;
                data.Loan = request.Loan;
                data.CurrencyId = request.CurrencyId;
                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
