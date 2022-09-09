using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatt.Models.Enums;

namespace Yatt.Models.Entities
{
    public class UserRole
    {
        [Key]
        [ForeignKey("User")]
        public string? UserId { get; set; }
        [Range(101, 104, ErrorMessage = "Role is required")]
        public RoleType Role { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifyDate { get; set; }
        // NAVIGATION
        public virtual User? User { get; set; }
    }
}
