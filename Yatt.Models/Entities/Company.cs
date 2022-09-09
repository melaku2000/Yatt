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
    public class Company
    {
        [Key]
        [ForeignKey("User")]
        public string? Id { get; set; }
        [StringLength(20)]
        public string? CompanyTin { get; set; }
        [Required]
        public CompanyStatus Status { get; set; }

        // NAVIGATION
        public virtual User? User { get; set; }
        public virtual CompanyDetail? CompanyDetail { get; set; }
    }
}
