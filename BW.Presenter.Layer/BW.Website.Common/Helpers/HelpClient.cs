using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;


namespace BW.Website.Common.Helpers
{
    class HelpClient
    {

        public HttpClient ConnectClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:4693/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        public HttpResponseMessage GetReponse(string path)
        {
            HttpClient client = ConnectClient();
            HttpResponseMessage reponse = client.GetAsync(path).Result;
            return reponse;
        }
    }
}
