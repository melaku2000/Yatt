using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatt.Models.Entities;
using Yatt.Models.Enums;

namespace Yatt.Models.Dtos
{
    public class UserTokenDto : BaseModel
    {
        public static implicit operator UserTokenDto(UserToken token)
        {
            return new UserTokenDto
            {
                Id = token.Id,
                UserId = token.UserId,
                TokenExpireTime = token.TokenExpireTime,
                CreatedDate = token.CreatedDate,
                DeletedDate = token.DeletedDate,
                IpAddress = token.IpAddress,
                ModifyDate = token.ModifyDate,
                Token = token.Token,
                TokenType = token.TokenType,
                UserAgent = token.UserAgent
            };
        }
        public string? UserId { get; set; }
        public string? Token { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public TokenType TokenType { get; set; }
        public DateTime TokenExpireTime { get; set; }
    }
}
