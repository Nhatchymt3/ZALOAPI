using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XProject.Core
{
    public class Endpoints
    {
        public const string Api = "Auth/";
        public static class HomeEndpoint
        {
            public const string Area = "";

            public const string BaseEndpoint = "~/" + Area;



        }
        public static class UserEndpoint
        {
            public const string Area = Api + "";

            public const string BaseEndpoint = "~/" + Area;

            public const string GetUser = "~/" + Area + "get-getuser";

            public const string Register = "~/" + Area + "register";

            public const string Changepassword = "~/" + Area + "change_password";

        }
        public static class AuthEndpoint
        {
            public const string Area = Api + "";

            public const string BaseEndpoint = "~/" + Area;

            public const string Token = "~/" +"Mail/Send";

            public const string Refesh = "~/" + Area + "refesh";
        }

        public static class MailEndpoint
        {
            public const string Area = "ApiPortal/";

            public const string Base = "~/" + Area + "mail/";
            public const string GetTemplate = Base + "gettemplate/";
        }
    }
}
