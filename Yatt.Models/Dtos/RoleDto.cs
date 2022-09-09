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
    public class RoleDto
    {
        public static implicit operator RoleDto(UserRole role)
        {
            return new RoleDto
            {
                UserId = role.UserId,
                Role = role.Role,
                CreatedDate = role.CreatedDate,
                ModifyDate = role.ModifyDate,
            };
        }
        public string? UserId { get; set; }
        [Range(101, 104, ErrorMessage = "Role is required")]
        public RoleType Role { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
