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
    public class SupplierPaymentCommand : IRequest<Supplier>
    {
        public int Id { get; set; }

        public double Loan { get; set; }
        public class SupplierPaymentCommandHandler : IRequestHandler<SupplierPaymentCommand, Supplier>
        {
            private readonly XanAutoDbContext db;

            public SupplierPaymentCommandHandler(XanAutoDbContext db)
            {
                this.db = db;
            }
            public async Task<Supplier> Handle(SupplierPaymentCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Suppliers.FirstOrDefaultAsync(s => s.Id == request.Id && s.DeletedDate == null, cancellationToken);
                if (data == null)
                {
                    return null;
                }

                if (request.Loan < data.Loan)
                {
                    var newsum = data.Loan - request.Loan;
                    data.Loan = newsum;
                    await db.SaveChangesAsync(cancellationToken);
                    return data;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
