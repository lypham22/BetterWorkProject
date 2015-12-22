using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using BW.Data.Contract.DTOs;
using System.Net;
using System.Configuration;
using System.Security.Cryptography;


namespace BW.Website.Common.Utilities
{
    public class ApiServiceUtilities
    {

        public static HttpClient ConnectClient()
        {
            var url = ConfigurationManager.AppSettings["BWApiServiceUrl"];
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //var credentials = Encoding.ASCII.GetBytes("myUsername:myPassword");
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials));
            return client;
        }

        public static HttpResponseMessage GetReponse(string path)
        {
            HttpClient client = ConnectClient();
            client.DefaultRequestHeaders.Add("ApiKey", ComputeHash());
            HttpResponseMessage reponse = client.GetAsync(path).Result;
            return reponse;
        }

        public static HttpResponseMessage PostJson(string path, object value)
        {
            HttpClient client = ConnectClient();
            client.DefaultRequestHeaders.Add("ApiKey", ComputeHash());
            HttpResponseMessage reponse = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable);
            try
            {
                reponse = client.PostAsJsonAsync(path, value).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode()).Result;
                return reponse;
            }
            catch (AggregateException e)
            {
                return reponse;
            }
        }

        public static string ComputeHash( )
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

            return hashString + "ApiKey" + DateTime.UtcNow.Ticks; ;
        }

        public static string  ParsePath()
        {
            string path="";
            return path;
        }
    }
}
