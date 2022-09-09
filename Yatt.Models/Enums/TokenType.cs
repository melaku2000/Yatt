using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatt.Models.Enums
{
    public enum TokenType
    {
        [StringValue("Email confermation")] EmailConfirmation = 411,
        [StringValue("Phone confermation")] PhoneConfiration = 412,
        [StringValue("Password reset")] PasswordReset = 413,
        [StringValue("Approve")] Approve = 444
    }
}
