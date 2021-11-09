using Newtonsoft.Json;
using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebTonicAssessment.ApiAccessMethods
{
    public class AuditControllerAccessMethods : WebTonicApiClient
    {
        private static string apiBaseUrl = "Audit";
        public static Audit CreateAudit(Audit request)
        {
            HttpResponseMessage httpResponseMessage = GetHttpClient().PostAsync(apiBaseUrl + "/PostAudit/", CreateContentString(request)).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                HttpContent httpContent = httpResponseMessage.Content;
                var results = httpContent.ReadAsStringAsync().Result;
                Audit cs = JsonConvert.DeserializeObject<Audit>(results);
                return cs;
            }
            else
            {
                return request;
            }
        }

        public static List<Audit> GetAllAudits()
        {
            List<Audit> ce = new List<Audit>();
            HttpResponseMessage response = GetHttpClient().GetAsync(apiBaseUrl + "/GetAllAudits").Result;
            if (response.IsSuccessStatusCode)
            {
                HttpContent content = response.Content;
                var results = content.ReadAsStringAsync().Result;
                ce = JsonConvert.DeserializeObject<List<Audit>>(results);
            }
            return ce;
        }

        public static List<Audit> GetAuditByClient(string client)
        {
            List<Audit> ce = new List<Audit>();
            HttpResponseMessage response = GetHttpClient().GetAsync(apiBaseUrl + "/GetAuditByClient/" + client).Result;
            if (response.IsSuccessStatusCode)
            {
                HttpContent content = response.Content;
                var results = content.ReadAsStringAsync().Result;
                ce = JsonConvert.DeserializeObject<List<Audit>>(results);
            }
            return ce;
        }  

        public static List<Audit> GetAuditByUserEmail(string email)
        {
            List<Audit> ce = new List<Audit>();
            HttpResponseMessage response = GetHttpClient().GetAsync(apiBaseUrl + "/GetAuditByUserEmail/" + email).Result;
            if (response.IsSuccessStatusCode)
            {
                HttpContent content = response.Content;
                var results = content.ReadAsStringAsync().Result;
                ce = JsonConvert.DeserializeObject<List<Audit>>(results);
            }
            return ce;
        }

        public static List<Audit> GetAuditByUserId(int userId)
        {
            List<Audit> ce = new List<Audit>();
            HttpResponseMessage response = GetHttpClient().GetAsync(apiBaseUrl + "/GetAuditByUserId/" + userId).Result;
            if (response.IsSuccessStatusCode)
            {
                HttpContent content = response.Content;
                var results = content.ReadAsStringAsync().Result;
                ce = JsonConvert.DeserializeObject<List<Audit>>(results);
            }
            return ce;
        }
      
    }
}
