using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebTonicAssessment.ApiAccessMethods
{
  
    public  class WebTonicApiClient
    {
        private static HttpClient client;

        //Function that creates and return the httpclient to be used.
        public static HttpClient GetHttpClient()
        {
            if (client != null)
            {
                return client;
            }
            else
            {
                //var endPoint = "http://bcx-assignment-api.tstsoftware.co.za/";
                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslpolisyErrors) =>
                {
                    return true;
                };
                client = new HttpClient(httpClientHandler);
                client.BaseAddress = new Uri("https://localhost:44312/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                return client;
            }
        }

        //The function take the post request and create create json string content to pass to the api.
        public static StringContent CreateContentString(object request)
        {
            var requestAsJsonString = JsonConvert.SerializeObject(request);
            var content = new StringContent(requestAsJsonString);
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
            return content;
        }
    }
}
