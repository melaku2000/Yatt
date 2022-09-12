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
    public class VacancyDto : BaseModel
    {
        public static implicit operator VacancyDto(Vacancy vacancy)
        {
            return new VacancyDto
            {
                Id = vacancy.Id,
                SubscrioptionId = vacancy.SubscrioptionId,
                ApplyUrl = vacancy.ApplyUrl,
                CreatedDate = vacancy.CreatedDate,
                DeadLineDate = vacancy.DeadLineDate,
                DeletedDate = vacancy.DeletedDate,
                Description = vacancy.Description,
                ModifyDate = vacancy.ModifyDate,
                Status = vacancy.Status
            };
        }
        [Required]
        public string? SubscrioptionId { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(250)]
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Deadline date is required")]
        public DateTime DeadLineDate { get; set; }
        public string? ApplyUrl { get; set; }
        public RowStatus Status { get; set; }
        // EXTENDED
    }
}
