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
    public class ExperianceDto : BaseModel
    {
        public static implicit operator ExperianceDto(Experiance experiance)
        {
            var dto= new ExperianceDto
            {
                Id = experiance.Id,
                DomainId = experiance.DomainId,
                CompanyName = experiance.CompanyName,
                CompanyPhone = experiance.CompanyPhone,
                Address = experiance.Address,
                CandidateId = experiance.CandidateId,
                Level = experiance.Level,
                HiringDate = experiance.HiringDate,
                LastDate = experiance.LastDate,
                Occupation = experiance.Occupation,
                CreatedDate = experiance.CreatedDate,
                ModifyDate = experiance.ModifyDate,
                DeletedDate = experiance.DeletedDate
            };
            if (experiance.Domain != null)
                dto.DomainName = experiance.Domain.Name;
            return dto;
        }
        [Required]
        public string? CandidateId { get; set; }
        [Required(ErrorMessage = "Domain is required")]
        public int DomainId { get; set; }
        [Required(ErrorMessage = "Company name is required")]
        [StringLength(50)]
        public string? CompanyName { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? CompanyPhone { get; set; }
        [StringLength(100)]
        public string? Address { get; set; }
        [Required]
        [Range(221, 225, ErrorMessage = "Experiance level is required")]
        public ExperianceLevel Level { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The maximum string length is 50 character")]
        public string? Occupation { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Hiring date is required")]
        public DateTime HiringDate { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Last date is required")]
        public DateTime LastDate { get; set; }

        // EXTENDED
        public string? DomainName { get; set; }
        public virtual List<DomainDto> Domains { get; set; } = null!;
    }
}
