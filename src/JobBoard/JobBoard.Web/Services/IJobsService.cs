using JobBoard.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobBoard.Web.Services
{
    public interface IJobsService
    {

        Task<Job> GetJobById(int id);
        Task<List<Job>> GetAllJobs();
        Task<Job> CreateJob(Job job);
        Task<Job> EditJob(Job job);
        Task<Job> DeleteJob(int id);
       
    }
}
