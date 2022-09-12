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
    public class JobDutyDto : BaseModel
    {
        public static implicit operator JobDutyDto(JobDuty job)
        {
            return new JobDutyDto
            {
                Id = job.Id,
                JobId = job.JobId,
                Duty= job.Duty,
                CreatedDate = job.CreatedDate,
                DeletedDate = job.DeletedDate,
                ModifyDate = job.ModifyDate,
            };
        }
        [Required]
        public string? JobId { get; set; }
        [Required(ErrorMessage = "Duty is required")]
        [StringLength(250)]
        public string? Duty { get; set; }
        // NAVIGATIONS
    }
}
