using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatt.Models.Entities;

namespace Yatt.Models.Dtos
{
    public class JobDescrioptionDto : BaseModel
    {
        public static implicit operator JobDescrioptionDto(JobDescription job)
        {
            return new JobDescrioptionDto
            {
                Id = job.Id,
                JobId = job.JobId,
                Descripttion = job.Descripttion,
                CreatedDate = job.CreatedDate,
                DeletedDate = job.DeletedDate,
                ModifyDate = job.ModifyDate,
            };
        }
        [Required]
        public string? JobId { get; set; }
        [Required(ErrorMessage = "Descripttion is required")]
        [StringLength(250)]
        public string? Descripttion { get; set; }
        // EXTENDED
    }
}
