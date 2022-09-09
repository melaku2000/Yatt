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
    public class CandidateDto
    {
        public static implicit operator CandidateDto(Candidate candidate)
        {
            return new CandidateDto
            {
                Id = candidate.Id,
                CountryId = candidate.CountryId,
                MobilePhone = candidate.MobilePhone,
                FirstName = candidate.FirstName,
                FatherName = candidate.FatherName,
                Gender = candidate.Gender,
                DoBirth = candidate.DoBirth,
                Address = candidate.Address,
                CreatedDate = candidate.CreatedDate,
                ModifyDate = candidate.ModifyDate,
                ShowDoBirth = candidate.ShowDoBirth,
                ShowPhone = candidate.ShowDoBirth,
                Email = candidate.User?.Email
            };
        }
        
        [Required]
        public string? Id { get; set; }
        [StringLength(50)]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage ="Country is required")]
        public int CountryId { get; set; }
        [StringLength(20)]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage ="Mobile phone number is required")]
        public string? MobilePhone { get; set; }
        [StringLength(30)]
        [Required(ErrorMessage ="First name is required")]
        public string? FirstName { get; set; }
        [StringLength(30)]
        [Required(ErrorMessage ="Father or Last name is required")]
        public string? FatherName { get; set; }
        [Required(ErrorMessage ="Gender is required")]
        public Gender Gender { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage ="Date of birth is required")]
        public DateTime DoBirth { get; set; }
        [StringLength(100)]
        public string? Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public bool ShowPhone { get; set; }
        public bool ShowDoBirth { get; set; }
    }
}
