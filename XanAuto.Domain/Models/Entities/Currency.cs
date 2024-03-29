﻿using System.Collections.Generic;
using XanAuto.Domain.AppCode.Infrastructure;

namespace XanAuto.Domain.Models.Entities
{
    public class Currency : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual ICollection<ProductCatalogItem> ProductCatalogItem { get; set; }
    }
}
