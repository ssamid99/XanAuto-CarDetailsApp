using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace XanAuto.Domain.Models.Entities.Membership
{
    public class XanAutoUser : IdentityUser<int>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [DefaultValue(false)]
        public bool AdminPermit { get; set; }
    }
}
