using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatt.Models.Enums
{
    public enum JobType
    {
        [StringValue("Part time")] PartTime=231,
        [StringValue("Full time")] FullTime=232,
        [StringValue("Contrat")] Remote=233,
        [StringValue("Remote part time")] RemotePartTime=234,
        [StringValue("Remote full time")] RemoteFullTime=235,
        [StringValue("Remote contrat")] RemoteContrat=236,
        [StringValue("Others")] Others=237,
    }
}
