using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XanAuto.Domain.AppCode.Infrastructure;

namespace XanAuto.Domain.Models.Entities
{
    public class ProductCatalogItem : BaseEntity
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int MeasureId { get; set; }
        public virtual Measure Measure { get; set; }
        public int CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
