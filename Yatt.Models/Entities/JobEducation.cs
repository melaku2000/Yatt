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
    public class JobEducation:BaseModel
    {
        [ForeignKey("Job")]
        public string? JobId { get; set; }
        [Required(ErrorMessage = "Education level is required")]
        public EducationLevel Level { get; set; }
        [StringLength(50)]
        public string? FieldOfStudy { get; set; }
        public byte YearsOfExperiance { get; set; }
        // NAVIGATIONS
        public virtual Job? Job { get; set; }
    }
}
