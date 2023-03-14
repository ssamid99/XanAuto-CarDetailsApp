using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XanAuto.Domain.AppCode.Infrastructure;

namespace XanAuto.Domain.Models.Entities
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<ProductCatalogItem> ProductCatalogItem { get; set; }
    }
}
