using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatt.Models.Enums;

namespace Yatt.Models.Entities
{
    public class UserToken : BaseModel
    {
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public string? Token { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public TokenType TokenType { get; set; }
        public DateTime TokenExpireTime { get; set; }
        // NAVIGATION
        public virtual User? User { get; set; }
    }
}
