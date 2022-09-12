using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatt.Models.Entities;

namespace Yatt.Models.Dtos
{
    public class JobQualificationDto : BaseModel
    {
        public static implicit operator JobQualificationDto(JobQualification job)
        {
            return new JobQualificationDto
            {
                Id = job.Id,
                JobId = job.JobId,
                Qualification = job.Qualification,
                CreatedDate = job.CreatedDate,
                DeletedDate = job.DeletedDate,
                ModifyDate = job.ModifyDate,
            };
        }
        [Required]
        public string? JobId { get; set; }
        [Required(ErrorMessage = "Qualification is required")]
        [StringLength(250)]
        public string? Qualification { get; set; }
        // NAVIGATIONS
        public virtual Job? Job { get; set; }
    }
}
