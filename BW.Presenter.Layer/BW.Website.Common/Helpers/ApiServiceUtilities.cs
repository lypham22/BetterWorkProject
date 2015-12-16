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


namespace BW.Website.Common.Helpers
{
    public class ApiServiceUtilities
    {
        public static HttpClient ConnectClient()
        {
            var url = ConfigurationManager.AppSettings["BWApiServiceUrl"];
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        public static HttpResponseMessage GetReponse(string path)
        {
            HttpClient client = ConnectClient();
            HttpResponseMessage reponse = client.GetAsync(path).Result;
            return reponse;
        }

        public static HttpResponseMessage PostJson(string path, object value)
        {
            HttpClient client = ConnectClient();
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
    }
}
