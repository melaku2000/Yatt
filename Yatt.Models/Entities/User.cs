using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatt.Models.Entities
{
    public class User:BaseModel
    {
        [StringLength(50)]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneConfirmed { get; set; }
        public int LockCount { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public byte[]? PasswordHash { get; set; }

        // NAVIGATION
        public virtual Candidate? Candidate { get; set; }
        public virtual Company? Company { get; set; }
        public virtual Admin? Admin { get; set; }
        public virtual UserRole? Role { get; set; }
        public virtual ICollection<RefreshToken>? RefreshTokens { get; set; }
    }
}
