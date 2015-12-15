using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using BW.Data.Contract.DTOs;


namespace BW.Website.Common.Helpers
{
    public class HelpClient
    {

        public static HttpClient ConnectClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8793/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        public static HttpResponseMessage GetReponse(string path)
        {
            HttpClient client = ConnectClient();
            HttpResponseMessage reponse = client.GetAsync(path).Result;
            return reponse;
        }

        public static void PostUserInfo(string path,User user)
        {
            HttpClient client = ConnectClient();
            
            try
            {
                client.PostAsJsonAsync(path, user).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            }
            catch (AggregateException e)
            {

            }
        }
    }
}
