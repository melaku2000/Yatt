using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatt.Models.Enums
{
    public enum ResponseStatus
    {
        NotFound = 301,
        Success = 302,
        Unautorize = 303,
        Error = 304
    }
}
