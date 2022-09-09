using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatt.Models.Enums;

namespace Yatt.Models.Dtos
{
    public class ResponseDto<T> where T : class
    {
        public T? Model { get; set; }
        public string? Message { get; set; }
        public ResponseStatus Status { get; set; }
    }
}
