using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XanAuto.Application.AppCode.Extensions;
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
        public int MeasureId { get; set; }
        public int CurrencyId { get; set; }
        public int SupplierId { get; set; }
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }
        public class ProductPutCommandHandler : IRequestHandler<ProductPutCommand, Product>
        {
            private readonly XanAutoDbContext db;
            private readonly IHostEnvironment env;

            public ProductPutCommandHandler(XanAutoDbContext db, IHostEnvironment env)
            {
                this.db = db;
                this.env = env;
            }
            public async Task<Product> Handle(ProductPutCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Products.Include(p=>p.ProductCatalogItem).FirstOrDefaultAsync(p => p.Id == request.Id && p.DeletedDate == null, cancellationToken);
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

                var catalog = await db.ProductCatalogItem.FirstOrDefaultAsync(c => c.ProductId == request.Id && c.DeletedDate == null, cancellationToken);

                if(catalog == null)
                {
                    return null;
                }

                if(request.MeasureId != 0 || request.CurrencyId != 0 || request.SupplierId != 0)
                {
                   db.ProductCatalogItem.Remove(catalog);
                    catalog.ProductId = request.Id;
                    catalog.MeasureId = request.MeasureId;
                    catalog.CurrencyId = request.CurrencyId;
                    catalog.SupplierId = request.SupplierId;
                    await db.ProductCatalogItem.AddAsync(catalog, cancellationToken);
                }


                if (request.Image == null)
                    goto end;

                string extension = Path.GetExtension(request.Image.FileName); //.jpg-ni goturur
                request.ImagePath = $"product-{Guid.NewGuid().ToString().ToLower()}{extension}";

                string fullPath = env.GetImagePhysicalPath(request.ImagePath);

                using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    request.Image.CopyTo(fs);
                }

                string oldPath = env.GetImagePhysicalPath(data.ImagePath);

                System.IO.File.Move(oldPath, env.GetImagePhysicalPath($"archive{DateTime.Now:yyyyMMdd}-{data.ImagePath}"));

                data.ImagePath = request.ImagePath;

            end:
                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
