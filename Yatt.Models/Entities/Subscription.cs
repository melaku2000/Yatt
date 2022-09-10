using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatt.Models.Enums;

namespace Yatt.Models.Entities
{
    public class Subscription : BaseModel
    {
        [ForeignKey("Company")]
        public string? CompanyId { get; set; }
        [ForeignKey("Membership")]
        public string? MembershipId { get; set; }
        // NAVIGATION
        public virtual Membership? Membership { get; set; }
        public virtual Company? Company { get; set; }
    }
}
