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
    public class SubscriptionDto : BaseModel
    {
        public static implicit operator SubscriptionDto(Subscription subscription)
        {
            var dto= new SubscriptionDto
            {
                Id = subscription.Id,
                CompanyId = subscription.CompanyId,
                MembershipId = subscription.MembershipId,
                Amount = subscription.Amount,
                NoOfCandidateInterview = subscription.NoOfCandidateInterview,
                NoOfJobPost = subscription.NoOfJobPost,
                ServicePeriodInMonth = subscription.ServicePeriodInMonth,
                CreatedDate = subscription.CreatedDate,
                ModifyDate = subscription.ModifyDate,
                DeletedDate = subscription.DeletedDate,
                Status = subscription.Status,
            };
            if(subscription.Membership!=null) dto.MembershipName = subscription.Membership.Name;
            return dto;
        }
        [Required]
        public string? CompanyId { get; set; }
        [Required]
        public string? MembershipId { get; set; }
        // TO NOTICE REGISTERED MEMBERSHIP VALUE IN CASE ITS CHANGED BY ADMIN
        public byte ServicePeriodInMonth { get; set; }
        public int NoOfJobPost { get; set; }
        public int NoOfCandidateInterview { get; set; }
        public decimal Amount { get; set; }
        public ClientStatus Status { get; set; }

        // EXTENDED
        public string? MembershipName { get; set; }
        public virtual List<MembershipDto> Memberships { get; set; } = null!;
    }
}
