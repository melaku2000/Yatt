using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatt.Models.Dtos
{
    public class FileData
    {
        public long UserId { get; set; }
        public string? FileBase64data { get; set; }
        public bool IsFirst { get; set; } = false;
        public string? DataType { get; set; }
        public long Offset { get; set; }
    }
}
