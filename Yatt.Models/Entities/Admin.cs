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
    public class Admin
    {
        [Key]
        [ForeignKey(nameof(User))]
        public string? Id { get; set; }
        [ForeignKey(nameof(Registerar))]
        public string? RegisterarId { get; set; }
        [ForeignKey("Country")]
        public int CountryId { get; set; }
        [Required]
        [StringLength(20)]
        [DataType(DataType.PhoneNumber)]
        public string? MobilePhone { get; set; }
        [StringLength(30)]
        public string? FirstName { get; set; }
        [StringLength(30)]
        public string? FatherName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public ClientStatus Status { get; set; }
        // NAVIGATION
        public virtual User? User { get; set; }
        [NotMapped]
        public virtual User? Registerar { get; set; }
        public virtual Country? Country { get; set; }
        public virtual ICollection<Payment>? Payments { get; set; }
    }
}
