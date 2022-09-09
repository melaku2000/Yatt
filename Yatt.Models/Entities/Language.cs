using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatt.Models.Entities
{
    public class Language
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string? EnglishName { get; set; }
        public virtual ICollection<UserLanguage>? Languages { get; set; }

    }
}
