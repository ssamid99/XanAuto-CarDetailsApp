using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XanAuto.Application.AppCode.Infrastructure;
using XanAuto.Domain.AppCode.Infrastructure;

namespace XanAuto.Domain.Models.Entities
{
    public class Product : BaseEntity, IPageable
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
        public Model Model { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int MeasureId { get; set; }
        public Measure Measure { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
