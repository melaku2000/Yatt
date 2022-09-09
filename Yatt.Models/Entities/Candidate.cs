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
    public class Candidate
    {
        [Key]
        [ForeignKey("User")]
        public string? Id { get; set; }
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
        [Required]
        public Gender Gender { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DoBirth { get; set; }
        [StringLength(100)]
        public string? Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public bool ShowPhone { get; set; }
        public bool ShowDoBirth { get; set; }

        // NAVIGATION
        public virtual User? User { get; set; }
        public virtual Country? Country { get; set; }
        public virtual ICollection<Education>? Educations { get; set; }
        public virtual ICollection<Experiance>? Experiances { get; set; }
    }
}
