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
    public class CompanyDetail
    {
        [Key]
        [ForeignKey("Company")]
        public string? CompanyId { get; set; }
        [ForeignKey("Domain")]
        public int DomainId { get; set; }
        [ForeignKey("Country")]
        public int CountryId { get; set; }
        [StringLength(20)]
        public string? ContactPhone { get; set; }
        [StringLength(100)]
        public string? ContactName { get; set; }
        [StringLength(50)]
        public string? CompanyName { get; set; }
        [StringLength(20)]
        public string? CompanyPhone { get; set; } 
        [StringLength(100)]
        public string? Address { get; set; }
        [StringLength(100)]
        public string? Description { get; set; }
        [StringLength(100)]
        public string? WebUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifyDate { get; set; }

        // NAVIGATION
        public virtual Company? Company { get; set; }
        public virtual Country? Country { get; set; }
        public virtual Domain? Domain { get; set; }
    }
}
