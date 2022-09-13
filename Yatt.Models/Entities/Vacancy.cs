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
    public class Vacancy : BaseModel
    {
        [ForeignKey("Subscription")]
        public string? SubscrioptionId { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(250)]
        public string? Description { get; set; }
        
        [DataType(DataType.Date)]
        [Required (ErrorMessage = "Deadline date is required")]
        public DateTime DeadLineDate { get; set; }
        public string? ApplyUrl { get; set; }
        public RowStatus Status { get; set; }
        // NAVIGATION
        public virtual Subscription? Subscription { get; set; }
        public virtual ICollection<Job>? Jobs { get; set; }
    }
}
