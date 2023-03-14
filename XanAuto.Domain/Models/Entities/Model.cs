using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XanAuto.Domain.AppCode.Infrastructure;

namespace XanAuto.Domain.Models.Entities
{
    public class Model : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<ProductCatalogItem> ProductCatalogItem { get; set; }
    }
}
