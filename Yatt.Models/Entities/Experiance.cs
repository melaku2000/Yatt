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
    public class Experiance : BaseModel
    {
        [ForeignKey("Candidate")]
        public string? CandidateId { get; set; }
        [ForeignKey("Domain")]
        public int DomainId { get; set; }
        [Required(ErrorMessage ="Company name is required")]
        [StringLength(50)]
        public string? CompanyName { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? CompanyPhone { get; set; }
        [StringLength(100)] 
        public string? Address { get; set; }
        [Required]
        [Range(221,225,ErrorMessage = "Experiance level is required")]
        public ExperianceLevel Level { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The maximum string length is 50 character")] 
        public string? Occupation { get; set; }
        [DataType(DataType.Date)]
        [Required (ErrorMessage = "Hiring date is required")]
        public DateTime HiringDate { get; set; }
        [DataType(DataType.Date)]
        [Required (ErrorMessage = "Last date is required")]
        public DateTime LastDate { get; set; }
        // NAVIGATION
        public virtual Candidate? Candidate { get; set; }
        public virtual Domain? Domain { get; set; }
    }
}
