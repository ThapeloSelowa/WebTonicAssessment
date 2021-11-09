using Newtonsoft.Json;
using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebTonicAssessment.ApiAccessMethods
{
    public class ErrorLogsControllerAccessMethods : WebTonicApiClient
    {
        private static string apiBaseUrl = "ErrorLogs";
        public static Error_log CreateErrorLog(Error_log request)
        {
            HttpResponseMessage httpResponseMessage = GetHttpClient().PostAsync(apiBaseUrl + "/PostErrorLog/", CreateContentString(request)).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                HttpContent httpContent = httpResponseMessage.Content;
                var results = httpContent.ReadAsStringAsync().Result;
                Error_log cs = JsonConvert.DeserializeObject<Error_log>(results);
                return cs;
            }
            else
            {
                return request;
            }
        }

        public static List<Error_log> GetAllErrorLogss()
        {
            List<Error_log> ce = new List<Error_log>();
            HttpResponseMessage response = GetHttpClient().GetAsync(apiBaseUrl + "/GetAllErrorLogs").Result;
            if (response.IsSuccessStatusCode)
            {
                HttpContent content = response.Content;
                var results = content.ReadAsStringAsync().Result;
                ce = JsonConvert.DeserializeObject<List<Error_log>>(results);
            }
            return ce;
        }

        public static List<Error_log> GetErrorLogsByClientName(string client)
        {
            List<Error_log> ce = new List<Error_log>();
            HttpResponseMessage response = GetHttpClient().GetAsync(apiBaseUrl + "/GetErrorLogsByClientName/" +client).Result;
            if (response.IsSuccessStatusCode)
            {
                HttpContent content = response.Content;
                var results = content.ReadAsStringAsync().Result;
                ce = JsonConvert.DeserializeObject<List<Error_log>>(results);
            }
            return ce;
        }
    }
}
