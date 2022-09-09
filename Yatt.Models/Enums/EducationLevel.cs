using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatt.Models.Enums
{
    public enum EducationLevel
    {
        [StringValue("None")] None = 201,
        [StringValue("Elementary")] Elementary = 202,
        [StringValue("Juniour Secondary")] JuniourSecondary = 203,
        [StringValue("Seniour Secondary")] SeniourSecondary = 204,
        [StringValue("Diploma")] Diploma = 205,
        [StringValue("Bachelor of Education")] BachelorOfEducation = 206,
        [StringValue("Bachelor of Art")] BachelorOfArt = 207,
        [StringValue("Masters")] Masters = 208,
        [StringValue("Doctorate")] Doctorate = 209,
        [StringValue("Others")] Others = 210
    }
}
