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
    public class JobApplicationDto
    {
        public static implicit operator JobApplicationDto(JobApplication job)
        {
            return new JobApplicationDto
            {
                Id = job.Id,
                JobId = job.JobId,
                CandidateId = job.CandidateId,
                CreatedDate = job.CreatedDate,
                ModifyDate = job.ModifyDate,
                Status = job.Status
            };
        }
        public string? Id { get; set; }
        [Required]
        public string? JobId { get; set; }
        [Required]
        public string? CandidateId { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ModifyDate { get; set; }
        [Range(121, 123, ErrorMessage = "Status is required")]
        public RowStatus Status { get; set; }
    }
}
