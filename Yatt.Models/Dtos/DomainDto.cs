using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatt.Models.Entities;

namespace Yatt.Models.Dtos
{
    public class DomainDto
    {
        public static implicit operator DomainDto(Domain domain)
        {
            return new DomainDto
            {
                Id = domain.Id,
                Name = domain.Name,
                CreatedDate = domain.CreatedDate,
                ModifyDate = domain.ModifyDate,
                DeletedDate = domain.DeletedDate
            };
        }
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
