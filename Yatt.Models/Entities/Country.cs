using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatt.Models.Entities
{
    public class Country
    {
        [Key]
        [Range(1, 1000)]
        public int Id { get; set; }
        [StringLength(10)]
        public string? DialCode { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }
    }
}
