using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatt.Models.Entities;
using Yatt.Models.Enums;

namespace Yatt.Models.Dtos
{
    public class UserDto:BaseModel
    {
        public static implicit operator UserDto(User user)=>
            new()
            {
                Id=user.Id,
                Email=user.Email, 
                CreatedDate=DateTime.Now ,
                ModifyDate=user.ModifyDate, 
                DeletedDate=user.DeletedDate, 
                EmailConfirmed=user.EmailConfirmed, 
                PhoneConfirmed=user.PhoneConfirmed,
                LockCount=user.LockCount,
                LastLoginTime=user.LastLoginTime,
                Role=user.Role!.Role.ToString()
            };
        public string? Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneConfirmed { get; set; }
        public int LockCount { get; set; }
        public DateTime LastLoginTime { get; set; }
        public string? Role{ get; set; }
    }
}
