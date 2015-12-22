using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
//using System.Runtime.Caching;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;


namespace Hmac.Api.Filters
{
    public class AuthenticateAttribute : ActionFilterAttribute
    {


        private static string ComputeHash()
        {
            string hashedPassword = ConfigurationManager.AppSettings["password"];
            string username = ConfigurationManager.AppSettings["username"];
            var key = Encoding.UTF8.GetBytes(hashedPassword.ToUpper());
            string hashString;

            using (var hmac = new HMACSHA256(key))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(username));
                hashString = Convert.ToBase64String(hash);
            }

            return hashString;
        }



        private static bool IsDateValidated(string timestampString)
        {


            // bool isDateTime = DateTime.TryParseExact(DateFromTimestamp(timestampString).tos, "U", null, DateTimeStyles.AdjustToUniversal, out timestamp);


            //if (!isDateTime)
            //    return false;

            DateTime timestamp = new DateTime(long.Parse(timestampString));

            var now = DateTime.UtcNow;

            // TimeStamp should not be in 5 minutes behind
            if (timestamp < now.AddMinutes(-5))
                return false;

            if (timestamp > now.AddMinutes(5))
                return false;

            return true;
        }

        private bool IsAuthenticated(HttpActionContext actionContext)
        {
            string path = "";
            IEnumerable<string> values;
            if (actionContext.Request.Headers.TryGetValues("ApiKey", out values))
            {
                path = ((string[])(values))[0];
            }
            else
            {
                return false;
            }
            string[] arr = path.Split(new string[] { "ApiKey" }, StringSplitOptions.None);
            if (arr.Length > 1)
            {
                var verifiedHash = ComputeHash();
                if (arr[0] != null && arr[0].Equals(verifiedHash))
                {
                    if (!IsDateValidated(arr[1]))
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var isAuthenticated = IsAuthenticated(actionContext);

            if (!isAuthenticated)
            {
                var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                actionContext.Response = response;
            }
        }
    }
}
