using Newtonsoft.Json;
using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebTonicAssessment.ApiAccessMethods
{
    public class StudentRecordsControllerMethods : WebTonicApiClient
    {

        private static string apiBaseUrl = "api/StudentsRecords";

        public static StudentRecord CreateOrUpdateStudentRecord(StudentRecord request)
        {
            if (request.Id == 0)
            {
                HttpResponseMessage httpResponseMessage = GetHttpClient().PostAsync(apiBaseUrl + "/PostStudentRecord/", CreateContentString(request)).Result;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    HttpContent httpContent = httpResponseMessage.Content;
                    var results = httpContent.ReadAsStringAsync().Result;
                    StudentRecord cs = JsonConvert.DeserializeObject<StudentRecord>(results);
                    return cs;
                }
                else
                {
                    return request;
                }
            }
            else
            {
                HttpResponseMessage httpResponseMessage = GetHttpClient().PutAsync(apiBaseUrl + "/PutStudentRecord/" + request.Id, CreateContentString(request)).Result;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    HttpContent httpContent = httpResponseMessage.Content;
                    var results = httpContent.ReadAsStringAsync().Result;
                    StudentRecord studentRecord = JsonConvert.DeserializeObject<StudentRecord>(results);
                    return studentRecord;
                }
                else
                {
                    return request;
                }
            }

        }

        public static List<StudentRecord> GetAllStudentsRecords()
        {
            List<StudentRecord> ce = new List<StudentRecord>();
            HttpResponseMessage response = GetHttpClient().GetAsync(apiBaseUrl + "/GetAllStudentsRecords").Result;
            if (response.IsSuccessStatusCode)
            {
                HttpContent content = response.Content;
                var results = content.ReadAsStringAsync().Result;
                ce = JsonConvert.DeserializeObject<List<StudentRecord>>(results);
            }
            return ce;
        }

        public static StudentRecord GetStudentRecordById(int id)
        {
            StudentRecord ce = new StudentRecord();
            HttpResponseMessage response = GetHttpClient().GetAsync(apiBaseUrl + "/GetStudentRecordById/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                HttpContent content = response.Content;
                var results = content.ReadAsStringAsync().Result;
                ce = JsonConvert.DeserializeObject<StudentRecord>(results);
            }
            return ce;
        }

        public static List<StudentRecord> CreateBulkStudentRecords(List<StudentRecord> requests)
        {

            HttpResponseMessage httpResponseMessage = GetHttpClient().PostAsync(apiBaseUrl + "/PostStudentRecord/", CreateContentString(requests)).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                HttpContent httpContent = httpResponseMessage.Content;
                var results = httpContent.ReadAsStringAsync().Result;
                List<StudentRecord> cs = JsonConvert.DeserializeObject<List<StudentRecord>>(results);
                return cs;
            }
            else
            {
                return requests;
            }
        }

    }

}

