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
    public class UserLanguage
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        [ForeignKey("Language")]
        public int LangId { get; set; }
        [Range(171, 175, ErrorMessage = "Language is required.")]
        public LanguageLevel Level { get; set; }
        // NAVIGATION
        public virtual User? User { get; set; }
        public virtual Language? Language { get; set; }
    }
}
