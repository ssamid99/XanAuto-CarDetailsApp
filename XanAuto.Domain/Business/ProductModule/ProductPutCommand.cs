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

namespace XanAuto.Domain.Business.ProductModule
{
    public class ProductPutCommand : IRequest<Product>
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Brand { get; set; }
        public string Oe { get; set; }
        public string Name { get; set; }
        public bool Important { get; set; }
        public bool Discount { get; set; }
        public bool Active { get; set; }
        public bool Original { get; set; }
        public int Amount { get; set; }
        public int Norm { get; set; }
        public int Order { get; set; }
        public double Cost { get; set; }
        public double SellingPrice { get; set; }
        public double RetailPrice { get; set; }
        public string Tag { get; set; }
        public string Description { get; set; }
        public int ModelId { get; set; }
        public int GroupId { get; set; }
        public int MeasureId { get; set; }
        public int CurrencyId { get; set; }
        public int SupplierId { get; set; }
        public class ProductPutCommandHandler : IRequestHandler<ProductPutCommand, Product>
        {
            private readonly XanAutoDbContext db;

            public ProductPutCommandHandler(XanAutoDbContext db)
            {
                this.db = db;
            }
            public async Task<Product> Handle(ProductPutCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Products.FirstOrDefaultAsync(p => p.Id == request.Id && p.DeletedDate == null, cancellationToken);
                if(data == null)
                {
                    return null;
                }
                data.Code = request.Code;
                data.Brand = request.Brand;
                data.Oe = request.Oe;
                data.Name = request.Name;
                data.Important = request.Important;
                data.Discount = request.Discount;
                data.Active = request.Active;
                data.Original = request.Original;
                data.Amount = request.Amount;
                data.Norm = request.Norm;
                data.Order = request.Order;
                data.Cost = request.Cost;
                data.SellingPrice = request.SellingPrice;
                data.RetailPrice = request.RetailPrice;
                data.Tag = request.Tag;
                data.Description = request.Description;
                data.ModelId = request.ModelId;
                data.GroupId = request.GroupId;
                data.MeasureId = request.MeasureId;
                data.CurrencyId = request.CurrencyId;
                data.SupplierId = request.SupplierId;

                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
