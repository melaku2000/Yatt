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
    public class Membership : BaseModel
    {
        [Required(ErrorMessage ="Membership name is required")]
        [StringLength(50)]
        public string? Name { get; set; }
        [Range(1,24, ErrorMessage ="Service period is required")]
        public byte ServicePeriodInMonth { get; set; }
        [Required(ErrorMessage ="Job post number is required")]
        public int NoOfJobPost { get; set; }
        [Required(ErrorMessage = "Candidate interview number is required")]
        public int NoOfCandidateInterview { get; set; }
        [Required(ErrorMessage ="Amount is required")]
        public decimal Amount { get; set; }
        
        // NAVIGATION
        public virtual ICollection<Subscription>? Subscriptions { get; set; }
    }
}
