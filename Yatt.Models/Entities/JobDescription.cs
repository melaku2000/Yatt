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
    public class JobDescription:BaseModel
    {
        [ForeignKey("Job")]
        public string? JobId { get; set; }
        [Required(ErrorMessage = "Descripttion is required")]
        [StringLength(250)]
        public string? Descripttion { get; set; }
        // NAVIGATIONS
        public virtual Job? Job { get; set; }
    }
}
