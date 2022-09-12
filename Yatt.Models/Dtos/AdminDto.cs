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
    public class AdminDto
    {
        public static implicit operator AdminDto(Admin admin)
        {
            return new AdminDto
            {
                Id = admin.Id,
                CountryId = admin.CountryId,
                MobilePhone = admin.MobilePhone,
                FirstName = admin.FirstName, 
                FatherName = admin.FatherName,
                CreatedDate = admin.CreatedDate,
                ModifyDate = admin.ModifyDate,
                Email = admin.User?.Email,
                Status=admin.Status
            };
        }
        
        [Required]
        public string? Id { get; set; }
        [Required]
        public string? RegisterarId { get; set; }
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
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ModifyDate { get; set; }
        public ClientStatus Status { get; set; }
    }
}
