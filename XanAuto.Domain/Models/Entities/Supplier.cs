using XanAuto.Application.AppCode.Infrastructure;
using XanAuto.Domain.AppCode.Infrastructure;

namespace XanAuto.Domain.Models.Entities
{
    public class Supplier : BaseEntity, IPageable
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public double Loan { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
    }
}

