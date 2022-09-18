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
    public class JobApplication
    {
        [Key]
        public string? Id { get; set; }
        [ForeignKey("Job")]
        public string? JobId { get; set; }
        [ForeignKey("Candidate")]
        public string? CandidateId { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ModifyDate { get; set; }
        [Range(121,123,ErrorMessage ="Status is required")]
        public RowStatus Status { get; set; }
        // NAVIGATIONS
        public virtual Job? Job { get; set; }
        public virtual Candidate? Candidate { get; set; }
    }
}
