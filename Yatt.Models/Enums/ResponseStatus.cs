using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatt.Models.Enums
{
    public enum ResponseStatus
    {
        NotFound = 401,
        Success = 402,
        Unautorize = 403,
        Error = 404
    }
}
