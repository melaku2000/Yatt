using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatt.Models.Enums
{
    public enum RoleType
    {
        [StringValue("Client")] Client = 101,
        [StringValue("Super admin")] SuperAdmin = 102,
        [StringValue("Company employee")] Employeer = 103,
        [StringValue("Candidate")] Candidate = 104,
    }   
}
