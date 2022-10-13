using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatt.Models.Entities;
using Yatt.Models.Enums;

namespace Yatt.Models.Dtos
{
    public class JobEducationDto : BaseModel
    {
        public static implicit operator JobEducationDto(JobEducation job)
        {
            return new JobEducationDto
            {
                Id = job.Id,
                JobId = job.JobId,
                Level = job.Level,
                FieldOfStudy= job.FieldOfStudy,
                YearsOfExperiance= job.YearsOfExperiance,
                CreatedDate = job.CreatedDate,
                DeletedDate = job.DeletedDate,
                ModifyDate = job.ModifyDate,
            };
        }
        [Required]
        public string? JobId { get; set; }
        [Required(ErrorMessage = "Education level is required")]
        public EducationLevel Level { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "Field of study is required")]
        public string? FieldOfStudy { get; set; }
        public byte YearsOfExperiance { get; set; }
        // NAVIGATIONS
    }
}
