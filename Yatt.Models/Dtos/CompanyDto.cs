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
    public class CompanyDto
    {
        public static implicit operator CompanyDto(Company company)
        {
            var dto= new CompanyDto
            {
                Id = company.Id,
                CompanyTin = company.CompanyTin,
                Status = company.Status,
            };
            if (company.User != null)
            {
                dto.Email= company.User.Email;
                dto.EmailConfirmed=company.User.EmailConfirmed;
                dto.PhoneConfirmed=company.User.PhoneConfirmed;
                dto.CreatedDate=company.User.CreatedDate;
                dto.ModifyDate=company.User.ModifyDate;
            }
            if (company.CompanyDetail != null)
            {
                dto.DomainId= company.CompanyDetail.DomainId;
                dto.CountryId= company.CompanyDetail.CountryId;
                dto.CompanyName=company.CompanyDetail.CompanyName;
                dto.CompanyPhone=company.CompanyDetail.CompanyPhone;
                dto.ContactName=company.CompanyDetail.ContactName;
                dto.ContactPhone=company.CompanyDetail.ContactPhone;
                dto.Address=company.CompanyDetail.Address;
                dto.WebUrl=company.CompanyDetail.WebUrl;
            }
            return dto;
        }
        public static implicit operator CompanyDto(CompanyDetail model)
        {
            var comp = new CompanyDto
            {
                Id = model.CompanyId,
                CompanyName = model.CompanyName,
                CompanyPhone = model.CompanyPhone,
                ContactName = model.CompanyName,
                ContactPhone = model.ContactPhone,
                Address = model.Address,
                CountryId = model.CountryId,
                DomainId=model.DomainId
            };
            if (model.Domain != null)
            {
                comp.DomainName = model.Domain.Name;
            }
            if (model.Country != null)
            {
                comp.CountryName = model.Country.Name;
            }
            if (model.Company != null)
            {
                comp.CompanyTin=model.Company.CompanyTin;
                comp.Status = model.Company.Status;
                if (model.Company.User != null)
                {
                    comp.Email = model.Company.User.Email;
                    comp.EmailConfirmed = model.Company.User.EmailConfirmed;
                    comp.PhoneConfirmed = model.Company.User.PhoneConfirmed;
                }
            }
            
            return comp;
        }
        public string? Id { get; set; }
        public string? CompanyTin { get; set; }
        public CompanyStatus Status { get; set; }

        // USER
        public string? Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneConfirmed { get; set; }

        // COMPANY DETAIL
        [Required(ErrorMessage = "Domain is required")]
        public int DomainId { get; set; }
        [Required(ErrorMessage ="Country is required")]
        public int CountryId { get; set; }
        [StringLength(20)]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage ="Contacts phone number is required")]
        public string? ContactPhone { get; set; }
        [StringLength(60)]
        [Required(ErrorMessage = "Contacts name is required")]
        public string? ContactName { get; set; }
        [StringLength(60)]
        [Required(ErrorMessage = "Company name is required")]
        public string? CompanyName { get; set; }
        [StringLength(20)]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Company office phone is required")]
        public string? CompanyPhone { get; set; }
        [StringLength(100)]
        public string? Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifyDate { get; set; }
        [StringLength(100)]
        public string? WebUrl { get; set; }

        // EXTENDED
        public string? CountryName { get; set; }
        public string? DomainName { get; set; }


    }
    public class RegisterCompanyDto
    {
        [Required(ErrorMessage = "Company TIN number is required.")]
        [StringLength(20)]
        public string? CompanyTin { get; set; }
        [EmailAddress]
        [StringLength(30)]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
        [Compare(nameof(Password), ErrorMessage = "Password and confirm password didnt match.")]
        public string? ConfirmPassword { get; set; }
    }
}
