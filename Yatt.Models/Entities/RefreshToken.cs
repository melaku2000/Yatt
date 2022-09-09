using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatt.Models.Entities
{
    public class RefreshToken : BaseModel
    {
        public string? UserId { get; set; }
        public string? Token { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public DateTime TokenExpireTime { get; set; }
    }
}
