using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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
    public class ProductPostCommand : IRequest<Product>
    {
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
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }
        public class ProductPostCommandHandler : IRequestHandler<ProductPostCommand, Product>
        {
            private readonly XanAutoDbContext db;
            private readonly IActionContextAccessor ctx;
            private readonly IHostEnvironment env;

            public ProductPostCommandHandler(XanAutoDbContext db, IActionContextAccessor ctx, IHostEnvironment env)
            {
                this.db = db;
                this.ctx = ctx;
                this.env = env;
            }
            public async Task<Product> Handle(ProductPostCommand request, CancellationToken cancellationToken)
            {
                var entity = new Product();
                entity.Code = request.Code;
                entity.Brand = request.Brand;
                entity.Oe = request.Oe;
                entity.Name = request.Name;
                entity.Important = request.Important;
                entity.Discount = request.Discount;
                entity.Active = request.Active;
                entity.Original = request.Original;
                entity.Amount = request.Amount;
                entity.Norm = request.Norm;
                entity.Order = request.Order;
                entity.Cost = request.Cost;
                entity.SellingPrice = request.SellingPrice;
                entity.RetailPrice = request.RetailPrice;
                entity.Tag = request.Tag;
                entity.Description = request.Description;
                entity.CreatedByUserId = ctx.GetCurrentUserId();

                var catalog = new ProductCatalogItem();

                catalog.ProductId = entity.Id;
                catalog.ModelId = request.ModelId;
                catalog.GroupId = request.GroupId;
                catalog.MeasureId = request.MeasureId;
                catalog.CurrencyId = request.CurrencyId;
                catalog.SupplierId = request.SupplierId;

                if (request.Image == null)
                    goto end;

                string extension = Path.GetExtension(request.Image.FileName);//.jpg

                request.ImagePath = $"product-{Guid.NewGuid().ToString().ToLower()}{extension}";
                string fullPath = env.GetImagePhysicalPath(request.ImagePath);

                using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    request.Image.CopyTo(fs);
                }

                entity.ImagePath = request.ImagePath;

            end:

                await db.Products.AddAsync(entity, cancellationToken);
                await db.ProductCatalogItem.AddAsync(catalog, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);
                return entity;
            }
        }
    }
}
