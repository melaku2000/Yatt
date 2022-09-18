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
    public class Job:BaseModel
    {
        [ForeignKey("Vacancy")]
        public string? VacancyId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [StringLength(50)]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Job type is required")]
        public JobType JobType { get; set; }
        [Required(ErrorMessage = "Experiance level is required")]
        public ExperianceLevel Level { get; set; }
        [Range(1,1000000)]
        public decimal Salary { get; set; }
        public string? ApplayUrl { get; set; }
        public string? ApplayLocation { get; set; }
        public RowStatus Status { get; set; }
        // NAVIGATIONS
        public virtual Vacancy? Vacancy { get; set; }
        public virtual JobApplication? Application { get; set; }
        public virtual ICollection<JobDuty>? Duties { get; set; }
        public virtual ICollection<JobDescription>? Descriptions { get; set; }
        public virtual ICollection<JobQualification>? Qualifications { get; set; }
        public virtual ICollection<JobEducation>? Educations { get; set; }
    }
}
