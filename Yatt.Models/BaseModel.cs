using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatt.Models
{
    public class BaseModel
    {
        [Key]
        public string? Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
