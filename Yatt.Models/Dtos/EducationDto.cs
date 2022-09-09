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
    public class EducationDto : BaseModel
    {
        public static implicit operator EducationDto(Education edu)
        {
            return new EducationDto
            {
                Id = edu.Id,
                AcademyName = edu.AcademyName,
                AcademyPhone = edu.AcademyPhone,
                Address = edu.Address,
                CandidateId = edu.CandidateId,
                Level = edu.Level,
                FieldOfStudy = edu.FieldOfStudy,
                ComplitionDate = edu.ComplitionDate,
                Grade = edu.Grade,
                CreatedDate = edu.CreatedDate,
                ModifyDate = edu.ModifyDate,
                DeletedDate = edu.DeletedDate
            };
        }
        [Required]
        public string? CandidateId { get; set; }
        [Required(ErrorMessage = "Academy name is required")]
        [StringLength(50)]
        public string? AcademyName { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? AcademyPhone { get; set; }
        [StringLength(100)]
        public string? Address { get; set; }
        [Required]
        [Range(201, 210, ErrorMessage = "Education level is required")]
        public EducationLevel Level { get; set; }
        [StringLength(50, ErrorMessage = "The maximum string length is 50 character")]
        public string? FieldOfStudy { get; set; }
        [Range(1, 10000, ErrorMessage = "Invalid grade")]
        public decimal Grade { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Complition year is required")]
        public DateTime ComplitionDate { get; set; }
    }
}
