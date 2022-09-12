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
    public class Payment 
    {
        [Key]
        public string? Id { get; set; }
        [ForeignKey(nameof(Subscription))]
        public string? SubscriptionId { get; set; }
        [ForeignKey("Admin")]
        public string? AdminId { get; set; }
        // TO REGISTERED MEMBERSHIP VALUE IN CASE ITS CHANGED BY ADMIN
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ModifyDate { get; set; }
        // NAVIGATION
        public virtual Subscription? Subscription { get; set; }
        public virtual Admin? Admin { get; set; }
    }
}
