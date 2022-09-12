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
    public class JobDuty:BaseModel
    {
        [ForeignKey("Job")]
        public string? JobId { get; set; }
        [Required(ErrorMessage = "Duty is required")]
        [StringLength(250)]
        public string? Duty { get; set; } 
        // NAVIGATIONS
        public virtual Job? Job { get; set; }
    }
}
