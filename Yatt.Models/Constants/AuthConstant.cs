using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatt.Models.Constants
{
    public static class AuthConstant
    {
        public static string USER_DATA = "AuthData";
        public static string TOKEN = "AuthToken";
        public static string REFRESH_TOKEN = "RefreshToken";
        public static string JWT_AUTH_TYPE = "jwtAuthType";
    }
    public static class ClaimConstant
    {
        public static string EMAIL_CONFIRMED = "EmailConfirmed";
        public static string PHONE_CONFIRMED = "PhoneConfirmed";
        public static string FULL_NAME = "FullName";
        public static string COMPANY_NAME = "CompanyName";
    }
}
