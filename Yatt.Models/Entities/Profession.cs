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
    public class Profession
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("Domain")]
        public int DomainId { get; set; }
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        // NAVIGATION
        public virtual Domain? Domain { get; set; }
    }
}
