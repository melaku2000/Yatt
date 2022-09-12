using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatt.Models.Enums;

namespace Yatt.Models.Dtos
{
    public class JobDto : BaseModel
    {
        [Required]
        public string? VacancyId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [StringLength(50)]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Job type is required")]
        public JobType JobType { get; set; }
        [Required(ErrorMessage = "Experiance level is required")]
        public ExperianceLevel Level { get; set; }
        [Range(1, 1000000)]
        public decimal Salary { get; set; }
        public string? ApplayUrl { get; set; }
        public string? ApplayLocation { get; set; }
        public RowStatus Status { get; set; }
        // EXTENDED
    }
}
