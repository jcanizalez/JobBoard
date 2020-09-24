using JobBoard.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Web.Services
{
    public class JobService : IJobsService
    {
         
        readonly string _apiUrl;

        public JobService(IConfiguration configuration)
        {
            _apiUrl = configuration["ApiUrl"] + "api/Jobs/";
        }
        public async Task<Job> CreateJob(Job job)
        {
            Job receivedJob;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(job), Encoding.UTF8, "application/json");
 
                using (var response = await httpClient.PostAsync(_apiUrl, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedJob = JsonConvert.DeserializeObject<Job>(apiResponse);
                }
            }
            return receivedJob;
            
        }

        public async Task<Job> DeleteJob(int id)
        {
            Job receivedJob;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(_apiUrl + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedJob = JsonConvert.DeserializeObject<Job>(apiResponse);
                }
            }

            return receivedJob;
        }

        public async Task<Job> EditJob(Job job)
        {
            Job receivedJob;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(job), Encoding.UTF8, "application/json");

                using (var response =  httpClient.PutAsync(_apiUrl + job.Id, content).Result)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedJob = JsonConvert.DeserializeObject<Job>(apiResponse);
                }
            }
            return receivedJob;
        }

        public async Task<List<Job>> GetAllJobs()
        {
            List<Job> jobs;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_apiUrl))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    jobs = JsonConvert.DeserializeObject<List<Job>>(apiResponse);
                }
            }
            return jobs;
        }

        public async Task<Job> GetJobById(int id)
        {
            Job job;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_apiUrl + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    job = JsonConvert.DeserializeObject<Job>(apiResponse);
                }
            }
            return job;
        }
    }
}
