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
    public class AuthDto
    {
        public static implicit operator AuthDto(User user)
        {
            var dto = new AuthDto
            {
                Id = user.Id,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PhoneConfirmed = user.PhoneConfirmed,
            };
            if (user.Candidate != null)
            {
                dto.FullName = $"{user.Candidate.FirstName} {user.Candidate.FatherName}";
            }
            if (user.Company != null)
            {
                if (user.Company.CompanyDetail != null)
                {
                    dto.FullName = user.Company.CompanyDetail.ContactName;
                    dto.CompanyName = user.Company.CompanyDetail.CompanyName;
                }
            }
            if (user.Role != null)
            {
                dto.UserRole = user.Role.Role.ToString();
            }

            return dto;
        }
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? CompanyName { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime TokenExpireTime { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneConfirmed { get; set; }
        public bool IsAviliable { get; set; }
        public string? UserRole { get; set; }
    }

    public class RegisterDto
    {
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }
        
        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
        [Compare(nameof(Password), ErrorMessage = "Password and confirm password didnt match.")]
        public string? ConfirmPassword { get; set; }
    }
    public class LoginDto
    {
        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
    }
    public class ConfirmDto
    {
        public string? UserId { get; set; }
        [Required, MinLength(6)]
        public string? Token { get; set; }
    }
    public class RequestTokenDto
    {
        public string? UserId { get; set; }
        public string? Token { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public TokenType TokenType { get; set; }
        public DateTime TokenExpireTime { get; set; }
    }
    public class RefreshTokenDto : BaseModel
    {
        public static implicit operator RefreshTokenDto(RefreshToken token)
        {
            return new RefreshTokenDto
            {
                Id = token.Id,
                UserId = token.UserId,
                TokenExpireTime = token.TokenExpireTime,
                CreatedDate = token.CreatedDate,
                DeletedDate = token.DeletedDate,
                IpAddress = token.IpAddress,
                ModifyDate = token.ModifyDate,
                Token = token.Token,
                UserAgent = token.UserAgent
            };
        }
        public string? UserId { get; set; }
        public string? Token { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public DateTime TokenExpireTime { get; set; }
    }

    public class RequestRefreshTokenDto
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
