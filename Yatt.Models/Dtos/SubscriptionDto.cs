using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatt.Models.Entities;

namespace Yatt.Models.Dtos
{
    public class SubscriptionDto : BaseModel
    {
        public static implicit operator SubscriptionDto(Subscription subscription)
        {
            return new SubscriptionDto
            {
                Id = subscription.Id,
                CompanyId = subscription.CompanyId, 
                MembershipId = subscription.MembershipId, 
                CreatedDate = subscription.CreatedDate,
                ModifyDate = subscription.ModifyDate, 
                DeletedDate = subscription.DeletedDate,
            };
        }
        [Required]
        public string? CompanyId { get; set; }
        [Required]
        public string? MembershipId { get; set; }
    }
}
