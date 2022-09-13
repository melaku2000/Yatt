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
    public class PaymentDto
    {
        public static implicit operator PaymentDto(Payment payment)
        {
            var dto= new PaymentDto
            {
                Id = payment.Id, AdminId=payment.AdminId, 
                CreatedDate = payment.CreatedDate,
                ModifyDate = payment.ModifyDate,
            };
            if (payment.Subscription != null)
            {
                dto.ServicePeriodInMonth = payment.Subscription.ServicePeriodInMonth;
                dto.NoOfCandidateInterview = payment.Subscription.NoOfCandidateInterview;
                dto.NoOfJobPost=payment.Subscription.NoOfJobPost;
                dto.Amount = payment.Subscription.Amount;
                dto.Status= payment.Subscription.Status;
            }
            return dto;
        }
        public string? Id { get; set; }
        [Required]
        public string? SubscriptionId { get; set; }
        [Required]
        public string? AdminId { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ModifyDate { get; set; }



        // EXTENDED
        public byte ServicePeriodInMonth { get; set; }
        public int NoOfJobPost { get; set; }
        public int NoOfCandidateInterview { get; set; }
        public decimal Amount { get; set; }
        public ClientStatus? Status { get; set; }
        public string? MembershipName { get; set; }
    }
}
