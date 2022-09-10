using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatt.Models.Entities;

namespace Yatt.Models.Dtos
{
    public class MembershipDto : BaseModel
    {
        public static implicit operator MembershipDto(Membership domain)
        {
            return new MembershipDto
            {
                Id = domain.Id,
                Name = domain.Name, 
                Amount = domain.Amount, 
                NoOfCandidateInterview = domain.NoOfCandidateInterview,
                NoOfJobPost = domain.NoOfJobPost, 
                ServicePeriodInMonth = domain.ServicePeriodInMonth,
                CreatedDate = domain.CreatedDate,
                ModifyDate = domain.ModifyDate, 
                DeletedDate = domain.DeletedDate
            };
        }
        [StringLength(50)]
        public string? Name { get; set; }
        [Range(1, 24, ErrorMessage = "Service period is required")]
        public byte ServicePeriodInMonth { get; set; }
        [Required(ErrorMessage = "Job post number is required")]
        public int NoOfJobPost { get; set; }
        [Required(ErrorMessage = "Candidate interview number is required")]
        public int NoOfCandidateInterview { get; set; }
        [Required(ErrorMessage = "Amount is required")]
        public decimal Amount { get; set; }
    }
}
